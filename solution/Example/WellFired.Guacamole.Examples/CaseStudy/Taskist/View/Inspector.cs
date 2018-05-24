using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View
{
    public class Inspector : Views.View
    {
        public Inspector()
        {
            OutlineColor = UIColor.FromRGB(241, 241, 241);
            BackgroundColor = UIColor.FromRGB(250, 250, 250);
            MinSize = UISize.Of(300, 30);
            HorizontalLayout = LayoutOptions.Expand;
            VerticalLayout = LayoutOptions.Fill;
            Padding = UIPadding.With(30, 60, 10, 60);

            var collection = new ObservableCollection<Filter>
            {
                new Filter { FilterName = "Item 1", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(236, 142, 117)) },
                new Filter { FilterName = "Item 2", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(204, 204, 204)) },
                new Filter { FilterName = "Item 3", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(174, 199, 225)) },
                new Filter { FilterName = "Item 4", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(146, 229, 211)) },
                new Filter { FilterName = "Item 5", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(217, 171, 224)) },
                new Filter { FilterName = "Item 6", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(236, 142, 117)) },
                new Filter { FilterName = "Item 7", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(204, 204, 204)) },
                new Filter { FilterName = "Item 8", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(174, 199, 225)) },
                new Filter { FilterName = "Item 9", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(204, 204, 204)) },
                new Filter { FilterName = "Item 10", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(174, 199, 225)) },
                new Filter { FilterName = "Item 11", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(146, 229, 211)) },
                new Filter { FilterName = "Item 12", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(217, 171, 224)) },
                new Filter { FilterName = "Item 13", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(236, 142, 117)) },
                new Filter { FilterName = "Item 14", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(204, 204, 204)) },
                new Filter { FilterName = "Item 15", FilterImage = ImageSource.From(ImageShape.Circle, 6.0, UIColor.FromRGB(174, 199, 225)) }
            };

            Content = new LayoutView
            {
                OutlineColor = UIColor.FromRGB(250, 250, 250),
                BackgroundColor = UIColor.FromRGB(250, 250, 250),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical, 5),
                Children = {
                    new ListView
                    {
                        EntrySize = 38,
                        OutlineColor = UIColor.FromRGB(250, 250, 250),
                        BackgroundColor = UIColor.FromRGB(250, 250, 250),
                        HorizontalLayout = LayoutOptions.Fill,
                        VerticalLayout = LayoutOptions.Expand,
                        ItemTemplate = DataTemplate.Of(typeof(FilterCell)),
                        ItemSource = collection
                    },
                    new ButtonView
                    {
                        Text = "New"
                    }
                }
            };
        }
    }
}