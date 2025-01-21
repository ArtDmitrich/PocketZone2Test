using Services.ObjectPool;

namespace GameLogic.DroppedItem
{
    public class DroppedItemPoolManager : ItemPoolManager<DroppedItemPoolManager>
    {
        public DroppedItem GetDroppedItem(string droppedItemName)
        {
            return _poolManager.GetPooledItem<DroppedItem>(droppedItemName);
        }
    }
}
