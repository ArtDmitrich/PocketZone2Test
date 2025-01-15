using System.Collections.Generic;
using UnityEngine;

namespace Services.UI
{
    public class MediatorUI: MonoBehaviour, IMediatorUI
    {
        [SerializeField] private View _testView;
        
        private Dictionary<ViewType, IView> _views = new ();
        
        private IView _openView;

        private void Awake()
        {
            RegisterView(ViewType.Test, _testView);
        }

        public void OpenView(ViewType viewType)
        {
            var view = GetView(viewType);
            view.Show();
            _openView = view;
        }

        public void CloseView()
        {
            _openView.Hide();
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
