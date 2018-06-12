using System.Collections.Generic;

namespace LockstepSystem.Network
{
    public sealed class NetworkManager
    {
        private readonly object _recvLock = new object();
        private readonly Queue<NetworkPacket> _recvQueue = new Queue<NetworkPacket>();

        // TODO(pyoung): NetworkManager - lock queue.
        // TODO(pyoung): NetworkManager - sending / recieving.
        private readonly object _sendLock = new object();
        private readonly Queue<NetworkPacket> _sendQueue = new Queue<NetworkPacket>();
        private INetworkClient _network;

        public List<NetworkPacket> GetIncommingPackets()
        {
            List<NetworkPacket> ret = new List<NetworkPacket>();

            lock (this._recvLock)
            {
                while (this._recvQueue.Count > 0)
                {
                    ret.Add(this._recvQueue.Dequeue());
                }
            }

            return ret;
        }

        public void Send(NetworkPacket packet)
        {
            lock (this._sendLock)
            {
                this._sendQueue.Enqueue(packet);
            }
        }


        public void Tick()
        {
            // TODO(pyoung): NetworkManager - error handling.
            lock (this._sendLock)
            {
                if (this._sendQueue.Count != 0)
                {
                    while (this._sendQueue.Count > 0)
                    {
                        NetworkPacket packet = this._sendQueue.Dequeue();
                        this._network.Send(packet);
                    }
                }
            }
        }

        public void Start(INetworkClient client)
        {
            this._network = client;
            this._network.OnRecieved += OnRecieved;
        }

        private void OnRecieved(NetworkPacket packet)
        {
            lock (this._recvLock)
            {
                this._recvQueue.Enqueue(packet);
            }
        }

        public void Stop()
        {
            this._network.OnRecieved -= OnRecieved;
        }
    }
}