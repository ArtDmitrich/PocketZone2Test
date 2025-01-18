using UnityEngine;

namespace Characters.Characters
{
    public class CharacterModelPart : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private SpriteRenderer _item;

        public void ChangeBody(Sprite body)
        {
            _body.sprite = body;
        }

        public void ChangeItem(Sprite item)
        {
            _item.enabled = true;
            _item.sprite = item;
        }

        public void TakeOffItem()
        {
            _item.enabled = false;
        }
    }
}
