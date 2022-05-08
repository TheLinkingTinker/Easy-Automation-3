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
    partial class Program
    {
        public class Result
        {
            bool conclusion;
            public List<Loc> location = new List<Loc>();
            string error = "";
            string errorMessage = "";

            public Result()
            {
                SetLocation(Loc.Main);
            }

            public void SetLocation(Loc loc)
            {
                location.Add(loc);
            }

            public void Succeed()
            {
                conclusion = true;
                location.RemoveAt(location.Count() - 1);
            }

            public void Fail(string errorMsg)
            {
                conclusion = false;
                errorMessage = errorMsg;
            }

            public bool Succeeded()
            {
                return conclusion;
            }

            public bool Failed()
            {
                return !conclusion;
            }

            public string ErrorMessage()
            {
                error = "Error:\n";
                foreach (Loc item in location)
                {
                    error += item.ToString() + "/";
                }
                error = error.Substring(0, error.Length - 1);
                error += " - \n" + errorMessage + "\n\n";
                return error;
            }

        }
    }
}
