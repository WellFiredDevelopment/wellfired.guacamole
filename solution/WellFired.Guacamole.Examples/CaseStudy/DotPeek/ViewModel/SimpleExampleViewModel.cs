using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.DotPeek.ViewModel
{
    public class SimpleExampleViewModel : ObservableBase
    {
        private UIColor _deltaColor;

        public UIColor DeltaColor
        {
            get { return _deltaColor; }
            set { SetProperty(ref _deltaColor, value, nameof(DeltaColor)); }
        }
        
        public void CalculateColors()
        {
            var random = new System.Random();
            DeltaColor = random.Next(0, 2) == 0 ? UIColor.ForestGreen : UIColor.IndianRed;
        }
    }
}