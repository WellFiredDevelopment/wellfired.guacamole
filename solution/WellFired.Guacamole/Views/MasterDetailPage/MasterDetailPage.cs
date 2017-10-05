using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views.MasterDetailPage
{
    /// <summary>
    /// The master detail page is a typical MasterDetail page. It allows you to specify a LayoutableView for the Master and a layoutable view for the Detail.
    /// It's important that the user is aware they need to change the Detail themselves. For this, you can call SetDetail. if you're looking for a view that
    /// takes care of this for you, please use the less flexible but equaly as useful ListViewMasterDetailPage
    /// </summary>
    public class MasterDetailPage : Page
    {
        public MasterDetailPage(ILayoutable master, ILayoutable detail)
        {
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;
            
            Content = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill, 
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 0),
                Children = {
                    master,
                    detail
                }
            };
        }

        protected void SetDetail(ILayoutable layoutable)
        {
            ((View)layoutable).SetStyleDictionary(StyleDictionary);
            InvalidateRectRequest();
            ((LayoutView)Content).Children[1] = layoutable;
        }
    }
}