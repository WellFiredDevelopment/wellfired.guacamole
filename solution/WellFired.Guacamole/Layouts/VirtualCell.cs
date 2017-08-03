using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Layouts
{
    public class VirtualCell : IVirtualCell
    {
        public UIRect PositionInCell { get; private set; }
        public UIRect Rect { get; set; }
        public ILayoutable Layoutable { get; set; }

        public void CalculatePositionInCell()
        {
/*            if (Layoutable.HorizontalLayout != LayoutOptions.Center && Layoutable.VerticalLayout != LayoutOptions.Center)
            {
                PositionInCell = new UIRect(0, 0, Layoutable.RectRequest.Width, Layoutable.RectRequest.Height);
                return;
            }

            var rect = PositionInCell;
            rect.Width = Layoutable.RectRequest.Width;
            rect.Height = Layoutable.RectRequest.Height;
            
            if (Layoutable.HorizontalLayout == LayoutOptions.Center)
            {
                var halfContainer = Rect.Width / 2;
                var half = Layoutable.RectRequest.Width / 2;
                rect.X = halfContainer - half;
            }
            
            if (Layoutable.VerticalLayout == LayoutOptions.Center)
            {
                var halfContainer = Rect.Height / 2;
                var half = Layoutable.RectRequest.Height / 2;
                rect.Y = halfContainer - half;
            }
            */
            PositionInCell = UIRect.Zero;
        }
    }
}