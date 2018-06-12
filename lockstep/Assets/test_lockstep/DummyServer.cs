using System;
using System.Threading.Tasks;
using LockstepSystem.Network;

namespace test_lockstep
{
    public class DummyServer : INetworkClient
    {
        public async void Send(NetworkPacket packet)
        {
            await Task.Delay(millisecondsDelay: 100);
            OnRecieved(packet);
        }

        public event Action<NetworkPacket> OnRecieved = delegate { };
    }
}