using System.Collections.Generic;
using GameLogic.Characters;
using Services.ObjectPool.ZenjectVariant;
using UnityEngine;
using Zenject;

namespace GameLogic.EnemiesController
{
    public class EnemiesController : MonoBehaviour
    {
        private List<MeleeEnemyCharacter> _enemies;
        
        private MeleeEnemyCharacter.Pool _pool;

        [Inject]
        public void Construct(MeleeEnemyCharacter.Pool meleeEnemyPool)
        {
            _pool = meleeEnemyPool;
        }

        public void SpawnEnemies(Transform spawnPoint)
        {
            var enemy = _pool.Spawn();
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.transform.parent = transform;
            
            _enemies.Add(enemy);
            enemy.CharacterDead += EnemyDie;

            enemy.Initialize();
        }

        private void EnemyDie(Character enemy)
        {
            enemy.CharacterDead -= EnemyDie;
            _enemies.Remove(enemy as MeleeEnemyCharacter);
        }
    }
}
