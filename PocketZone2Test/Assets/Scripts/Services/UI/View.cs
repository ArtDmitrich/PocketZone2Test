using UnityEngine;

namespace Services.UI
{
    public class View : MonoBehaviour, IView
    {
        public ViewType ViewType => _viewType;
        
        [SerializeField] private ViewType _viewType;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
