using System.Collections.Generic;
using LockstepSystem.InputCommand;
using LockstepSystem.RenderCommand;

namespace LockstepSystem
{
    public abstract class BasePresentation<T> where T : InputCommandManager
    {
        protected BasePresentation(T t)
        {
            this.Input = t;
        }

        public abstract void RenderCommand(List<IRenderCommand> commands);
        public T Input { get; }
    }
}