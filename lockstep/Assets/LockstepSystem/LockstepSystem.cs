using System.Collections.Generic;
using LockstepSystem.InputCommand;
using LockstepSystem.Network;
using LockstepSystem.RenderCommand;
using LockstepSystem.TurnData;

namespace LockstepSystem
{
    public class LockstepSystem<TIcManager, TRcManager>
        where TIcManager : InputCommandManager
        where TRcManager : RenderCommandManager
    {
        public enum E_STATE
        {
            SUSPENDED,
            PLAYING,
            DELAYED,
        }

        private int _accumulatedMillisec;
        private int _accumulatedSubTurn;
        private int _currentTurn;

        protected LockstepSystem(string uuid, BasePresentation<TIcManager> presentation,
            BaseSimulation<TRcManager> simulation)
        {
            this.uuid = uuid;
            this._currentTurn = -this.Config.TurnGap;
            this._accumulatedMillisec = 0;
            this._accumulatedSubTurn = 0;
            this.Presentation = presentation;
            this.Simulation = simulation;
        }

        public string uuid { get; }
        public TIcManager Input => this.Presentation.Input;
        public BasePresentation<TIcManager> Presentation { get; }
        public BaseSimulation<TRcManager> Simulation { get; }
        public LockstepConfig Config { get; } = new LockstepConfig();
        public NetworkManager Network { get; } = new NetworkManager();
        public TurnDataManager TurnDataMgr { get; } = new TurnDataManager();

        public E_STATE State { get; private set; } = E_STATE.SUSPENDED;
        public bool IsRunning => this.State != E_STATE.SUSPENDED;

        public void Tick(int dt)
        {
            if (!this.IsRunning)
            {
                return;
            }

            this.Network.Tick();
            // ## update input commands.

            if (this.State == E_STATE.DELAYED)
            {
                UpdateDelay(dt);
            }
            else
            {
                WorldUpdate(dt);
                ProcessIncomingPackets(dt);
                SendOutgoingPackets(dt);
            }

            this.Presentation.RenderCommand(this.Simulation.Renderer.FlushCommands());
        }

        private void UpdateDelay(int dt)
        {
            ProcessIncomingPackets(dt);
            if (this.State == E_STATE.DELAYED)
            {
                // TODO(pyoung): heartbeat.
            }
        }

        private void SendOutgoingPackets(int dt)
        {
            this._accumulatedMillisec += dt;

            while (this._accumulatedMillisec > this.Config.MillisecPerSubTurn)
            {
                this._accumulatedMillisec -= this.Config.MillisecPerSubTurn;

                // ## Subturn.
                this._accumulatedSubTurn += 1;

                if (this._accumulatedSubTurn >= this.Config.SubTurnsPerTurn)
                {
                    // ## Stepping Turn.
                    {
                        int steppedTurn = this._currentTurn + this.Config.TurnGap;
                        List<IInputCommand> commands = this.Input.FlushCommands();

                        TurnData.TurnData turnData = new TurnData.TurnData(this.uuid, commands);

                        NetworkPacket packet =
                            new NetworkPacket(NetworkPacket.E_TYPE.TURN, steppedTurn, this.uuid, turnData);

                        this.Network.Send(packet);
                    }

                    if (this._currentTurn < 0)
                    {
                        this._accumulatedSubTurn = 0;
                        this._currentTurn += 1;
                    }
                    else
                    {
                        if (TryAdvanceTurn())
                        {
                            this._accumulatedSubTurn = 0;
                            this._currentTurn += 1;
                        }
                        else
                        {
                            this.State = E_STATE.DELAYED;
                        }
                    }

                    break;
                }
            }
        }

        private void WorldUpdate(int dt)
        {
        }

        private void ProcessIncomingPackets(int dt)
        {
            // TODO(pyoung): dispose input during delay.

            List<NetworkPacket> incommingPackets = this.Network.GetIncommingPackets();
            foreach (NetworkPacket packet in incommingPackets)
            {
                int turn = packet.Turn;
                string uuid = packet.uuid;
                TurnData.TurnData turnData = packet.TurnData;
                this.TurnDataMgr[turn].Add(uuid, turnData);

                if (this.State == E_STATE.DELAYED)
                {
                    if (TryAdvanceTurn())
                    {
                        this._accumulatedSubTurn = 0;
                        this._currentTurn += 1;
                    }
                    else
                    {
                        this.State = E_STATE.DELAYED;
                    }
                }
            }
        }

        private bool TryAdvanceTurn()
        {
            int nextTurn = this._currentTurn + 1;
            Dictionary<string, TurnData.TurnData> userTurnDatas = this.TurnDataMgr[nextTurn];

            if (userTurnDatas.Count != this.Config.PlayerCount)
            {
                return false;
            }

            if (this.State == E_STATE.DELAYED)
            {
                this.State = E_STATE.PLAYING;
                // TODO(pyoung): is need delay for waiting slow peer?
            }

            this.Simulation.TurnSimulate(nextTurn, userTurnDatas);

            return true;
        }

        public void Start(INetworkClient networkClient)
        {
            this.State = E_STATE.PLAYING;
            this.Network.Start(networkClient);
        }

        public void Stop()
        {
            this.State = E_STATE.SUSPENDED;
            this.Network.Stop();
        }
    }
}