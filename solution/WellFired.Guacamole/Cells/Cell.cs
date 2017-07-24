using System.ComponentModel;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Cells
{
    public partial class Cell : View, ICell
    {
        protected Cell()
        {
            
        }

        public IListView Container { get; set; }
        
        public virtual void RecycleWithNewBindingContext()
        {
            
        }

        public void ResetBindingContext(INotifyPropertyChanged notifyPropertyChanged)
        {
            BindingContext = notifyPropertyChanged;
            if(Content != null)
                ((View)Content).BindingContext = notifyPropertyChanged;
        }
    }
}