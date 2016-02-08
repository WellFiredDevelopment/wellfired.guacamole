namespace WellFired.Guacamole.Databinding
{
    public interface IBindingElement
    {
        BindingContext Context { get; }
        IObservableModel Model { get; set; }
        void OnBindingMessage(ObservableMessage message);
        void OnBindingRefresh();
    }
}