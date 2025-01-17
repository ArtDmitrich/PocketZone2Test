using Services.ObjectPool;

namespace GameLogic.Bullet
{
    public class BulletManager : ItemManager<BulletManager>
    {
        public Bullet GetBullet(string bulletName)
        {
            return _poolManager.GetPooledItem<Bullet>(bulletName);
        }
    }
}
