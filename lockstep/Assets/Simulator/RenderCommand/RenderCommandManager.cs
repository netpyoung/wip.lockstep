namespace Simulator.RenderCommand
{
    public sealed class RenderCommandManager : LockstepSystem.RenderCommand.RenderCommandManager
    {
        public void Move()
        {
            QueueCommand(new RC_Move());
        }
    }
}