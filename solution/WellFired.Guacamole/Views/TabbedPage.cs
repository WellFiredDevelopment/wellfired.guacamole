using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class TabbedPage : ItemsView
    {
        private readonly LayoutView _tabSelect;
        private readonly List<Page> _pages = new List<Page>();
        private readonly View _tabbedPageContent;

        public TabbedPage()
        {
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;

            _tabSelect = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal)
            };

            _tabbedPageContent = new View {HorizontalLayout = LayoutOptions.Fill, VerticalLayout = LayoutOptions.Fill};
            
            Content = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill, 
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
                Children = {
                    _tabSelect,
                    _tabbedPageContent
                }
            };
        }

        protected override void ItemSourceChanged()
        {
            BuildFromItemSource();
        }

        protected override void ItemSourceCleared()
        {
            BuildFromItemSource();
        }

        protected override void ItemAdded(object item, int index)
        {
            BuildFromItemSource();
        }

        protected override void ItemRemoved(object item)
        {
            BuildFromItemSource();
        }

        protected override void ItemReplaced(object oldItem, object newItem, int index)
        {
            BuildFromItemSource();
        }

        private void BuildFromItemSource()
        {   
            _tabSelect.Children.Clear();
            _pages.Clear();
            
            if (ItemTemplate == null)
                return;
            
            foreach (var bindingContext in ItemSource)
            {
                var newPage = ItemTemplate.Create(this, bindingContext);

                if (newPage.BindingContext != null)
                    throw new TabbedPagePageShouldntAlreadyHaveBindingContext(this, bindingContext, newPage);
                
                newPage.BindingContext = bindingContext as INotifyPropertyChanged;

                var button = new TabbedPageButton
                {
                    ButtonPressedCommand = new Command
                    {
                        CanExecute = true,
                        ExecuteAction = () => { Selected(bindingContext); }
                    }
                };

                if (!(newPage is Page))
                    throw new DataTemplateTypeDidntCreateAPage(this, bindingContext, newPage);

                var page = (Page) newPage;
                button.Text = page.Title;
                
                _tabSelect.Children.Add(button);
                _pages.Add(page);
            }

            Selected(SelectedPage);
            _tabSelect.InvalidateRectRequest();
        }

        public void Selected(object bindingContext)
        {
            TabbedButtonSelect(bindingContext);
            if (bindingContext == null)
            {
                _tabbedPageContent.Content = _pages.First();
                return;
            }

            var selectedEntry = _pages.First(o => Equals(o.BindingContext, bindingContext));
            _tabbedPageContent.Content = selectedEntry;
        }

        private void TabbedButtonSelect(object bindingContext)
        {
            var index = bindingContext == null ? 0 : ItemSource.IndexOf(bindingContext);
            foreach (var item in _tabSelect.Children)
                ((TabbedPageButton) item).IsSelected = false;
            
            ((TabbedPageButton)_tabSelect.Children[index]).IsSelected = true;
        }
    }
}