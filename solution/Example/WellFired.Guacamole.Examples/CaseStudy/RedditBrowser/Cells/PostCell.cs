using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.Cells
{
    /// <summary>
    /// This class defines the cell that is one entry in a View.
    /// Here we display the data for 1 post.
    /// </summary>
    public class PostCell : Cell
    {
        public PostCell()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Expand;
            
            var image = new ImageView
            {
                HorizontalLayout = LayoutOptions.Expand,
                MaxSize = UISize.Of(100),
                MinSize = UISize.Of(100)
            };
            
            var title = new LabelView {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill 
            };
            
            title.Bind(LabelView.TextProperty, "Title");
            image.Bind(ImageView.ImageSourceProperty, "Image");
            
            Content = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children =
                {
                    image,
                    title
                }
            };
        }
    }
}