using System.Collections.Generic;
using LockstepSystem;
using LockstepSystem.RenderCommand;
using Simulator.InputCommands;

namespace test_lockstep
{
    public class Presentation : BasePresentation<InputCommandManager>
    {
        public override void RenderCommand(List<IRenderCommand> commands)
        {
        }

        public Presentation(InputCommandManager t) : base(t)
        {
        }
    }
}