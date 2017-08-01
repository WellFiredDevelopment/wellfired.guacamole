using System.ComponentModel;

namespace WellFired.Guacamole.Views
{
    public partial class ToggleView : View
    {
        public ToggleView()
        {
            Style = Styling.Styles.ToggleView.Style;
        }
        
        public void Click()
        {
            FocusControl();

            if (!Enabled)
                return;

            On = !On;
            
            if (ButtonPressedCommand.CanExecute)
                ButtonPressedCommand.Execute();
        }

        private void CommandChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(ICommand.CanExecute))
                Enabled = ButtonPressedCommand.CanExecute;
        }
    }
}