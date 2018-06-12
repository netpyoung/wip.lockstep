namespace LockstepSystem
{
    // TODO(pyoung): struct?
    public sealed class LockstepConfig
    {
        public int MillisecPerSubTurn = 33;
        public int PlayerCount = 2;
        public int SubTurnsPerTurn = 3;
        public int TurnGap = 2;
        public int MillisecPerTurn => this.SubTurnsPerTurn * this.MillisecPerSubTurn;
    }
}