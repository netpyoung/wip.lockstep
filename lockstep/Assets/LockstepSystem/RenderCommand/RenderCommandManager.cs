using System.Collections.Generic;

namespace LockstepSystem.RenderCommand
{
    public abstract class RenderCommandManager
    {
        private readonly Queue<IRenderCommand> _queue = new Queue<IRenderCommand>();

        public List<IRenderCommand> FlushCommands()
        {
            List<IRenderCommand> ret = new List<IRenderCommand>();

            while (this._queue.Count > 0)
            {
                // TODO(pyoung): InputCommandManager - remove duplicates.
                ret.Add(this._queue.Dequeue());
            }

            return ret;
        }

        protected void QueueCommand<T>(T command) where T : struct, IRenderCommand
        {
            this._queue.Enqueue(command);
        }
    }
}