using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MelonLoader;
using InControl;

namespace MaquettePracticeMod
{
	class PracticeModController : MonoBehaviour
	{
		public static PracticeModController Instance;
		public GameObject player;

		private bool _noclip;
		private float _noclipSpeed = 10f;

		private Text _playerText;
		private GameObject _canvas;

		private Scene _currentScene;

		bool triggersVisible;
		List<GameObject> triggerGameObjects = new List<GameObject>();


		void Awake()
		{
			PracticeModController.Instance = this;
			SceneManager.sceneLoaded += OnSceneLoaded;

			_canvas = CreateCanvas();
			base.gameObject.AddComponent<LevelJumpingScript>();

			if (_global.player != null)
				player = _global.player.gameObject;

			_currentScene = SceneManager.GetActiveScene();
			triggerGameObjects = new List<GameObject>();
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (_global.player != null && _global.player.gameObject != player)
			{
				player = _global.player.gameObject;
			}
			_currentScene = SceneManager.GetActiveScene();
			CleaneTriggerObjectList();
		}

		private void CleaneTriggerObjectList()
		{
			foreach (GameObject gameObject in triggerGameObjects)
			{
				if (gameObject == null)
					triggerGameObjects.Remove(gameObject);
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.K))
				_noclip = !_noclip;

			if (_playerText != null)
				_playerText.text = "";

			if (player == null) { return; }

			if (Input.GetKeyDown(KeyCode.F11))
				triggersVisible = !triggersVisible;

			UpdateTriggerVisibility();

			_playerText.text = GetPlayerTextString();

			player.GetComponent<CharacterController>().enabled = !_noclip;


			if(_noclip)
			{
				player.GetComponent<FPController>().SetGravity(true);
				_noclipSpeed += Input.mouseScrollDelta.y;
				_noclipSpeed = Mathf.Max(0f, _noclipSpeed);
				Vector3 directionVector = new Vector3(_global.g.playerActions.move.X, 0f, _global.g.playerActions.move.Y);
				if (Input.GetKey(KeyCode.Space))
				{
					directionVector.y += 1f;
				}
				if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
				{
					directionVector.y -= 1f;
				}
				player.transform.Translate(directionVector.normalized * Time.deltaTime * _noclipSpeed);
			}
		}

		GameObject CreateCanvas()
		{
			GameObject myGO;
			GameObject myText;
			Canvas myCanvas;
			Text text;
			RectTransform rectTransform;

			// Canvas
			myGO = new GameObject();
			myGO.name = "PracticeModCanvas";
			myGO.AddComponent<Canvas>();
			DontDestroyOnLoad(myGO);

			myCanvas = myGO.GetComponent<Canvas>();
			myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
			myGO.AddComponent<CanvasScaler>();
			myGO.AddComponent<GraphicRaycaster>();

			Text playerText = NewPlayerText();
			_playerText = playerText;

			return myGO;
		}

		Text NewPlayerText()
		{
			Text newText;
			GameObject gameObject = new GameObject("PlayerText");
			gameObject.transform.parent = GameObject.Find("PracticeModCanvas").transform;
			CanvasGroup cg = gameObject.AddComponent<CanvasGroup>();
			cg.interactable = false;
			cg.blocksRaycasts = false;
			newText = gameObject.AddComponent<Text>();
			RectTransform component = newText.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2((float)(Screen.currentResolution.width / 3), (float)(Screen.currentResolution.height / 3));
			component.pivot = new Vector2(0f, 1f);
			component.anchorMin = new Vector2(0f, 1f);
			component.anchorMax = new Vector2(0f, 1f);
			component.anchoredPosition = new Vector2(25f, -25f);
			foreach (Font font in Resources.FindObjectsOfTypeAll<Font>())
			{
				if (font.name == "Arial")
				{
					newText.font = font;
				}
			}
			newText.text = "hello world";
			newText.fontSize = 30;

			return newText;
		}

		string GetPlayerTextString()
		{
			Vector3 position = player.transform.localPosition;
			Vector3 velocity = player.GetComponent<CharacterController>().velocity;
			Vector3 rotation = player.transform.localRotation.eulerAngles;
			float scale = player.transform.localScale.x;
			string dynamicInfo = "";



			if (_noclip)
				dynamicInfo += "\nNoClip";


			return string.Concat(new object[]
			{
				"Position: ",
				position.x.ToString("0.000"),
				", ",
				position.y.ToString("0.000"),
				", ",
				position.z.ToString("0.000"),
				"\n",
				"Rotation: ",
				Camera.main.transform.rotation.eulerAngles.x.ToString("0.000"),
				", ",
				rotation.y.ToString("0.000"),
				"\n",
				"Horizontal Velocity: ",
				Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z).ToString("0.000")+" m/s",
				"\n",
				"Vertical Velocity: ",
				velocity.y.ToString("0.000")+" m/s",
				"\n",
				dynamicInfo
			});
		}

		public void AddTriggerGO(GameObject go)
		{
			triggerGameObjects.Add(go);
		}


		private void UpdateTriggerVisibility()
		{
			foreach (GameObject gameObject in triggerGameObjects)
			{
				if(gameObject != null && gameObject.activeSelf != triggersVisible)
					gameObject.SetActive(triggersVisible);
			}
		}
	}


}
