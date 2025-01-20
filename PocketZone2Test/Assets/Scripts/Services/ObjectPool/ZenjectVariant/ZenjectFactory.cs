using UnityEngine;
using Zenject;

namespace Services.ObjectPool.ZenjectVariant
{
    public class ZenjectFactory<T> : PlaceholderFactory<T> where T : MonoBehaviour
    {
    }
}
