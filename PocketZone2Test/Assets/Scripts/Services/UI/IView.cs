namespace Services.UI
{
    public interface IView
    {
        ViewType ViewType { get; }
        void Show();
        void Hide();
    }
}
