using System.Collections.Generic;

namespace LockstepSystem.TurnData
{
    public sealed class TurnDataManager
    {
        // TODO(pyoung): TurnDataManager - dic cleanup.
        private readonly Dictionary<int, Dictionary<string, TurnData>> _dic =
            new Dictionary<int, Dictionary<string, TurnData>>();

        public Dictionary<string, TurnData> this[int key]
        {
            get
            {
                Dictionary<string, TurnData> ret;
                if (!this._dic.TryGetValue(key, out ret))
                {
                    ret = new Dictionary<string, TurnData>();
                    this._dic[key] = ret;
                }

                return ret;
            }
            set { this._dic[key] = value; }
        }
    }
}