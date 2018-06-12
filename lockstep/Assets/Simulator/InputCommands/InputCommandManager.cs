namespace Simulator.InputCommands
{
    public sealed class InputCommandManager : LockstepSystem.InputCommand.InputCommandManager
    {
        public void PressLeft()
        {
            QueueCommand(new IC_Left());
        }

        public void PressRight()
        {
            QueueCommand(new IC_Right());
        }
    }
}