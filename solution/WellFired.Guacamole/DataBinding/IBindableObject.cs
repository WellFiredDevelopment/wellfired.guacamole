using System.ComponentModel;

namespace WellFired.Guacamole.DataBinding
{
    public interface IBindableObject
    {
        INotifyPropertyChanged BindingContext { get; set; }
    }
}