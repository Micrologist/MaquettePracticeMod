using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaquettePracticeMod
{
	class LevelJumpingScript : MonoBehaviour
	{
		void Update()
		{
			if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) { return; }

			if (Input.GetKeyDown(KeyCode.Alpha1))
				_global.g.LoadAltLevel("Chapter_0");
			else if (Input.GetKeyDown(KeyCode.Alpha2))
				_global.g.LoadAltLevel("Chapter_1");
			else if (Input.GetKeyDown(KeyCode.Alpha3))
				_global.g.LoadAltLevel("Chapter_2");
			else if (Input.GetKeyDown(KeyCode.Alpha4))
				_global.g.LoadAltLevel("Chapter_3");
			else if (Input.GetKeyDown(KeyCode.Alpha5))
				_global.g.LoadAltLevel("Chapter_4");
			else if (Input.GetKeyDown(KeyCode.Alpha6))
				_global.g.LoadAltLevel("Chapter_5");
			else if (Input.GetKeyDown(KeyCode.Alpha7))
				_global.g.LoadAltLevel("Chapter_6");
			else if (Input.GetKeyDown(KeyCode.Alpha0))
				_global.g.LoadAltLevel("Title");

		}
	}
}
