namespace LockstepSystem.InputCommand
{
    public interface IInputCommand
    {
    }

    public interface IInputCommand<T> where T : BaseSimulation
    {
        void Influence(T t);
    }
}