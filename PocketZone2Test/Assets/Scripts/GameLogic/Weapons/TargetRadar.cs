using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public enum TargetType
    {
        Player,
        Enemy
    }
    
    public class TargetRadar : MonoBehaviour
    {
        public TargetType TargetType => _targetType;
        
        [SerializeField] private TargetType _targetType;
        
        private readonly List<Transform> _detectedTargets = new List<Transform>();
        
        public Transform GetNearestTarget()
        {
            if (_detectedTargets.Count == 0)
            {
                return null;
            }
            
            Transform nearestTarget = null;
            float nearestDistance = Mathf.Infinity;

            foreach (var target in _detectedTargets)
            {
                float distance = Vector2.Distance(transform.position, target.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = target;
                }
            }

            return nearestTarget;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag(_targetType.ToString()))
            {
                _detectedTargets.Add(collider.transform); 
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag(_targetType.ToString()))
            {
                _detectedTargets.Remove(collider.transform);
            }
        }
    }
}
