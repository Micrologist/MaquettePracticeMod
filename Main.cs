using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using Harmony;

[assembly: MelonInfo(typeof(MaquettePracticeMod.Main), "Maquette Practice Mod", "0.3.0", "Micrologist#2351")]
[assembly: MelonGame("Graceful Decay", "Maquette")]


namespace MaquettePracticeMod
{
    public class Main : MelonMod
    {

        public override void OnApplicationStart()
        {
            MPM_Patcher.Patch();
        }
        public override void OnLevelWasLoaded(int level)
        {
            if (_global.g != null && _global.g.gameObject.GetComponent<PracticeModController>() == null)
                _global.g.gameObject.AddComponent<PracticeModController>();

        }
    }
}
