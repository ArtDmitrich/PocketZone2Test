using Services.ObjectPool;

namespace GameLogic.Bullet
{
    public class BulletPoolManager : ItemPoolManager<BulletPoolManager>
    {
        public Bullet GetBullet(string bulletName)
        {
            return _poolManager.GetPooledItem<Bullet>(bulletName);
        }
    }
}
