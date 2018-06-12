namespace LockstepSystem.Network
{
    public struct NetworkPacket
    {
        public enum E_TYPE
        {
            TURN,
            DELAY,
        }

        public E_TYPE Type;
        public int Turn;
        public TurnData.TurnData TurnData;
        public string uuid;

        public NetworkPacket(E_TYPE type, int turn, string uuid, TurnData.TurnData turnData)
        {
            this.Type = type;
            this.Turn = turn;
            this.uuid = uuid;
            this.TurnData = turnData;
        }
    }
}