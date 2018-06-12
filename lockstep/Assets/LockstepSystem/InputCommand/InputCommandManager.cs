using System.Collections.Generic;

namespace LockstepSystem.InputCommand
{
    public class InputCommandManager
    {
        private readonly Queue<IInputCommand> _queue = new Queue<IInputCommand>();

        public List<IInputCommand> FlushCommands()
        {
            List<IInputCommand> ret = new List<IInputCommand>();
            if (this._queue.Count == 0)
            {
                ret.Add(new InputCommandNoOperation());
            }
            else
            {
                while (this._queue.Count > 0)
                {
                    // TODO(pyoung): InputCommandManager - remove duplicates.
                    ret.Add(this._queue.Dequeue());
                }
            }

            return ret;
        }

        protected void QueueCommand<T>(T command) where T : struct, IInputCommand
        {
            this._queue.Enqueue(command);
        }
    }
}