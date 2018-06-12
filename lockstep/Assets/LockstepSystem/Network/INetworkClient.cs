using System;

namespace LockstepSystem.Network
{
    public interface INetworkClient
    {
        void Send(NetworkPacket packet);
        event Action<NetworkPacket> OnRecieved;
    }
}