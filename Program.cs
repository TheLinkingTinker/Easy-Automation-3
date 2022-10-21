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
    partial class Program : MyGridProgram
    {
        static bool debug = true;
        static string debugBlockName = "Debug LCD";
        static int debugScreenNumber = 0;
        IMyTextSurfaceProvider debugBlock;
        IMyTextSurface debugScreen;

        IMyTerminalBlock codeSorce;
        string oldCode;
        public enum Loc
        {
            Main,
            GetCommand,
            Parse,
            ParseCommand,
            GetBlock,
            GetScreen,
            GetCustomData,
            GetCodeBlocks,
            Between
        }

        public Program()
        {
        }

        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {
            var result = new Result();

            if (debug && debugScreen == null){
                debugScreen = GetScreen(debugBlockName, debugScreenNumber, ref result);
                if (result.Failed()) { debugScreen = Me.GetSurface(0); result.Fail("Problem with setting debug screen"); goto End; }
            }

        End:
            codeSorce = null;
            oldCode = "";
            if (result.Failed()) { debugScreen.WriteText(result.ErrorMessage()); }
            debugScreen = null;
        }

        public IMyTextSurface GetScreen(string blockName, int screenNumber, ref Result result)
        {
            result.SetLocation(Loc.GetScreen);

            IMyTerminalBlock block = GetBlock(blockName, ref result);
            if (result.Failed()) { return Me.GetSurface(0); }
            if (!(block is IMyTextSurfaceProvider)){ result.Fail(blockName + " does not have any screens"); return Me.GetSurface(0); }

            IMyTextSurface surface = (block as IMyTextSurfaceProvider).GetSurface(screenNumber);
            if (surface == null) { result.Fail("The screen number " + screenNumber.ToString() + " does not exist on " + blockName) ; return Me.GetSurface(0); }

            return surface;
        }

        public IMyTerminalBlock GetBlock(string blockName, ref Result result)
        {
            result.SetLocation(Loc.GetBlock);

            var block = GridTerminalSystem.GetBlockWithName(blockName);
            if (block == null) { result.Fail("Block " + "\"" + blockName + "\"" + " not found, check case, spelling, and ownership"); return Me; }

            result.Succeed();
            return block;
        }

        public List<IMyTerminalBlock> GetBlocks(string blockName, ref Result result)
        {
            result.SetLocation(Loc.GetBlock);

            var blocks = new List<IMyTerminalBlock>();
            GridTerminalSystem.SearchBlocksOfName(blockName, blocks);
            if (blocks.Count < 1) { result.Fail("No blocks whith name " + "\"" + blockName + "\"" + " found, check case, spelling, and ownership"); return new List<IMyTerminalBlock>() { Me }; }

            result.Succeed();
            return blocks;
        }
    }
}
