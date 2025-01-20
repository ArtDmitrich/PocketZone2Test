using UnityEngine;
using UnityEngine.Pool;

namespace Services.ObjectPool
{
    public class PooledItem: MonoBehaviour
    {
        public IObjectPool<PooledItem> Pool;

        public void Release()
        {
            Pool.Release(this);
        }
    }
}
