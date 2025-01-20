using GameLogic.Characters;
using Services.ObjectPool.ZenjectVariant;

namespace GameLogic.EnemiesController
{
    public class MeleeEnemyPool : ZenjectPool<MeleeEnemyCharacter>
    {
        protected override void OnCreated(MeleeEnemyCharacter enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        protected override void OnSpawned(MeleeEnemyCharacter enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        protected override void OnDespawned(MeleeEnemyCharacter enemy)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
