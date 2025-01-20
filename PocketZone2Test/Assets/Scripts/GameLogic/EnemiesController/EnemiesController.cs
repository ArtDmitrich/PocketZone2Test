using System;
using System.Collections.Generic;
using GameLogic.Characters;
using Services.Logger;
using UnityEngine;

namespace GameLogic.EnemiesController
{
    public class EnemiesController : MonoBehaviour
    {
        public event Action AllEnemiesDie;
        
        private List<Character> _enemies = new List<Character>();
        
        public void SpawnEnemies(Vector2 spawnPoint, string enemyName)
        {
            var enemy = MeleeEnemiesPoolManager.Instance.GetMeleeEnemy(enemyName);
            
            if (enemy == null)
            {
                LoggerService.LogError($"{this.name} can`t spawn {enemyName}");
                return;
            }
            
            enemy.transform.position = spawnPoint;
            enemy.transform.parent = transform;
            
            _enemies.Add(enemy);
            enemy.CharacterDead += EnemyDie;

            enemy.Initialize();
        }

        private void EnemyDie(Character enemy)
        {
            enemy.CharacterDead -= EnemyDie;
            _enemies.Remove(enemy);

            if (_enemies.Count == 0)
            {
                AllEnemiesDie?.Invoke();
            }
        }
    }
}
