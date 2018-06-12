using System;
using System.Collections.Generic;
using System.Reflection;
using LockstepSystem;
using LockstepSystem.InputCommand;
using LockstepSystem.TurnData;
using Simulator.InputCommands;
using Simulator.RenderCommand;
using UnityEngine;

namespace Simulator
{
    public class Simulation : BaseSimulation<RenderCommandManager>
    {
        public Simulation(RenderCommandManager t) : base(t)
        {
        }

        public override void TurnSimulate(int turn, Dictionary<string, TurnData> turnDatas)
        {
            // setting turn data.
            foreach (KeyValuePair<string, TurnData> p in turnDatas)
            {
                string uuid = p.Key;
                TurnData turnData = p.Value;
                foreach (IInputCommand command in turnData.Commands)
                {
                    Debug.Log($"{uuid} - {command}");
                }
            }

            // generate render command.
        }
    }
}