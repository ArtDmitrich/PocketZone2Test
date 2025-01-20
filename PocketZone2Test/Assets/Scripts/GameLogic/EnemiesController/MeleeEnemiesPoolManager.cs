using GameLogic.Characters;
using Services.ObjectPool;

namespace GameLogic.EnemiesController
{
    public class MeleeEnemiesPoolManager : ItemPoolManager<MeleeEnemiesPoolManager>
    {
        public MeleeEnemyCharacter GetMeleeEnemy(string enemyName)
        {
            return _poolManager.GetPooledItem<MeleeEnemyCharacter>(enemyName);
        }
    }
}
