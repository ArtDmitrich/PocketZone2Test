using UnityEngine;

namespace GameLogic.Characters
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Transform _fillArea;
    
        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            float healthPercentage = currentHealth / maxHealth;
            _fillArea.localScale = new Vector3(healthPercentage, 1, 1);
            _fillArea.localPosition = new Vector3(-0.5f * (1 - healthPercentage), 0, 0);
        }
    }
}
