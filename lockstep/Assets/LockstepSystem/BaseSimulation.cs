using System.Collections.Generic;
using LockstepSystem.RenderCommand;

namespace LockstepSystem
{
    public abstract class BaseSimulation
    {
        public abstract void TurnSimulate(int turn, Dictionary<string, TurnData.TurnData> turnDatas);
    }

    public abstract class BaseSimulation<T> : BaseSimulation where T : RenderCommandManager
    {
        protected BaseSimulation(T t)
        {
            this.Renderer = t;
        }

        public T Renderer { get; }
    }
}