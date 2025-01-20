using UnityEngine;
using Zenject;

namespace Services.ObjectPool.ZenjectVariant
{
    public class ZenjectPool<T> : MemoryPool<T> where T : MonoBehaviour
    {
        protected override void OnCreated(T item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnDespawned(T item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(T item)
        {
            item.gameObject.SetActive(true);
        }
    }
}
