using System.Collections.Generic;
using UnityEngine;

namespace Services.UI
{
    public class ViewMediatorUI: MonoBehaviour, IViewMediatorUI
    {
        [SerializeField] private List<View> _viewsInScene  = new List<View>();
        
        private Dictionary<ViewType, IView> _views = new ();
        
        private IView _openView;

        private void Awake()
        {
            foreach (var view in _viewsInScene)
            {
                RegisterView(view.ViewType, view);
            }
        }

        public void OpenView(ViewType viewType)
        {
            CloseView();
            
            var view = GetView(viewType);
            view.Show();
            _openView = view;
        }

        private void CloseView()
        {
            _openView?.Hide();
            _openView = null;
        }

        private IView GetView(ViewType viewType)
        {
            return _views[viewType];
        }
        
        private void RegisterView(ViewType viewType, IView view)
        {
            _views.Add(viewType, view);
        }
    }
}
