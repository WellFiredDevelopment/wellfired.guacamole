using System.ComponentModel;

namespace WellFired.Guacamole.DataBinding
{
    public interface IBindableObject
    {
        INotifyPropertyChanged BindingContext { get; set; }

        object GetValue(BindableProperty bindableProperty);
        bool SetValue(BindableProperty bindableProperty, object value);
    }
}