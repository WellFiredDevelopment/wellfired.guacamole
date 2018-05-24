using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Pages.MasterDetailPage
{
    /// <summary>
    /// The master detail page is a typical MasterDetail page. It allows you to specify a LayoutableView for the Master and a layoutable view for the Detail.
    /// It's important that the user is aware they need to change the Detail themselves. For this, you can call SetDetail. if you're looking for a view that
    /// takes care of this for you, please use the less flexible but equaly as useful ListViewMasterDetailPage
    /// </summary>
    public class MasterDetailPage : Page
    {
        private readonly ViewContainer _detailContainer;
        
        public MasterDetailPage(ILayoutable master, IView detail)
        {    
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;
            
            _detailContainer = new ViewContainer{Content = detail};
            
            Content = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill, 
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 0),
                Children = {
                    master,
                    _detailContainer
                }
            };
        }

        protected void SetDetail(ILayoutable layoutable)
        {
            ((View)layoutable).SetStyleDictionary(StyleDictionary);
            _detailContainer.Content = (IView) layoutable;
            InvalidateRectRequest();
        }

        public override void InvalidateRectRequest()
        {
            base.InvalidateRectRequest();
            
            _detailContainer?.InvalidateRectRequest();
            ((View)_detailContainer?.Content)?.InvalidateRectRequest();
        }
    }
}