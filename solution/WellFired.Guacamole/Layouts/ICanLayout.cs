namespace WellFired.Guacamole.Layouts
{
    public interface ICanLayout : IHasChildren
    {
        ILayoutChildren Layout { get; set; }
    }
}
