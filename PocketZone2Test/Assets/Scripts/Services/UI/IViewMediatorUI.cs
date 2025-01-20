namespace Services.UI
{
    public interface IViewMediatorUI
    {
        void OpenView(ViewType viewType);
        void CloseView();
    }
}
