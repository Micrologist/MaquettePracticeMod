using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using System.Reflection;
using MelonLoader;
using UnityEngine;

namespace MaquettePracticeMod
{
	public class MPM_Patcher
	{
		public static void Patch()
		{
			var harmony = HarmonyInstance.Create("com.superliminalpracticemod.patch");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}


	[HarmonyPatch(typeof(EntryTrigger))]
	[HarmonyPatch("Start")]
	class TriggerEnterColliderPatch
	{
		static void Prefix(EntryTrigger __instance)
		{
			MelonLogger.Log("Entry Trigger is waking up.");
			if (!__instance.gameObject.GetComponent<Collider>())
				return;
			Bounds bounds = __instance.gameObject.GetComponent<Collider>().bounds;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.transform.position = bounds.center;
			gameObject.transform.localScale = bounds.extents * 2f;
			gameObject.transform.parent = __instance.gameObject.transform;
			gameObject.GetComponent<BoxCollider>().enabled = false;
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			component.material.shader = Shader.Find("Standard");
			component.GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 1f);
			gameObject.SetActive(false);
			PracticeModController.Instance.AddTriggerGO(gameObject);
		}
	}

	[HarmonyPatch(typeof(EndOfLevelTrigger))]
	[HarmonyPatch("Start")]
	class EndOfLevelTriggerPatch
	{
		static void Prefix(EndOfLevelTrigger __instance)
		{
			MelonLogger.Log("Entry Trigger is waking up.");
			if (!__instance.gameObject.GetComponent<Collider>())
				return;
			Bounds bounds = __instance.gameObject.GetComponent<Collider>().bounds;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.transform.position = bounds.center;
			gameObject.transform.localScale = bounds.extents * 2f;
			gameObject.transform.parent = __instance.gameObject.transform;
			gameObject.GetComponent<BoxCollider>().enabled = false;
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			component.material.shader = Shader.Find("Standard");
			component.GetComponent<MeshRenderer>().material.color = new Color(0f, 1f, 1f, 1f);
			gameObject.SetActive(false);
			PracticeModController.Instance.AddTriggerGO(gameObject);
		}
	}

}
