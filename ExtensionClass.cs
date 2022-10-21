using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    static class ExtensionClass
    {
        public static List<string> ToList(this String str, char spliter = ',', char[] spliters = null, bool trim = true)
        {
            string[] a;
            a = spliters != null ? str.Split(spliters) : str.Split(spliter);
            var b = a.ToList();
            if (trim) b = a.Select(p => p.Trim()).ToList();
            b.RemoveAll(x => string.IsNullOrEmpty(x));
            return b;
        }
    }
}
