using LockstepSystem;
using Simulator;
using Simulator.InputCommands;
using Simulator.RenderCommand;

namespace test_lockstep
{
    public sealed class Lockstep : LockstepSystem<InputCommandManager, RenderCommandManager>
    {
        public Lockstep(string uuid) : base(uuid, new Presentation(new InputCommandManager()),
            new Simulation(new RenderCommandManager()))
        {
        }
    }
}