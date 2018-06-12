using System.Collections.Generic;
using System.Text;
using LockstepSystem.InputCommand;

namespace LockstepSystem.TurnData
{
    public struct TurnData
    {
        // TODO(pyoung): TurnData CRC.
        // TODO(pyoung): TurnData RandomNumber.

        public List<IInputCommand> Commands;
        public string uuid;

        public TurnData(string uuid, List<IInputCommand> commands)
        {
            this.uuid = uuid;
            this.Commands = commands;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IInputCommand inputCommand in this.Commands)
            {
                if (inputCommand is InputCommandNoOperation)
                {
                    continue;
                }

                sb.AppendLine(inputCommand.ToString());
            }

            return sb.ToString();
        }
    }
}