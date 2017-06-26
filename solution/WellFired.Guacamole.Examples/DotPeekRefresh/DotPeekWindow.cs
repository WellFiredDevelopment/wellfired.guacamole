using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeekRefresh
{
    public class DotPeekRefreshWindow : Window
    {
        public DotPeekRefreshWindow()
        {
            Content = DotPeekRefreshPage.Create();
        }
    }
}