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
    }
}