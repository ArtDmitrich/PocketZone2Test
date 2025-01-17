using System;

namespace Services.ObjectPool
{
    [Serializable]
    public struct PooledItemData
    {
        public string Key;
        public PooledItem Value;
    }
}
