using System.ComponentModel;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.Cells;
using WellFired.Guacamole.Examples.CaseStudy.RedditBrowser.ViewModel;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.RedditBrowser
{
    public class RedditBrowserWindow : Window
    {
        public RedditBrowserWindow(ILogger logger, INotifyPropertyChanged persistantData) 
            : base(logger, persistantData)
        {
            var textEntry = new TextEntry { CornerRadius = 0 };
            var searchButton = new Button {Text = "Search", CornerRadius = 0};
            
            var listView = new ListView {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Orientation = OrientationOptions.Vertical,
                ItemTemplate = DataTemplate.Of(typeof(PostCell)),
                EntrySize = 100
            };
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
                Children =
                {
                    new LayoutView
                    {
                        HorizontalLayout = LayoutOptions.Fill,
                        VerticalLayout = LayoutOptions.Expand,
                        Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                        Children =
                        {
                            textEntry,
                            searchButton
                        }
                    },
                    listView
                }
            };
            
            // Bind the text entry to the SubReddit Property on the bindingContext SearchResult.
            textEntry.Bind(TextEntry.TextProperty, "SubReddit", BindingMode.TwoWay);
            
            // Bind the Button's command to the Search Property on the bindingContext SearchResult.
            searchButton.Bind(Button.ButtonPressedCommandProperty, "Search");
            
            // Bind the ListViews Items to the SearchResult Property on the bindingContext SearchResult.
            listView.Bind(ItemsView.ItemSourceProperty, "SearchResults");
            
            BindingContext = new SearchResult();
        }
    }
}