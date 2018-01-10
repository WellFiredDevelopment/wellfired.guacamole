using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Pages
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
            
            if (ItemTemplate == null || ItemSource == null)
                return;
            
            foreach (var bindingContext in ItemSource)
            {
                var newPage = ItemTemplate.Create(this, bindingContext);

                if (newPage.BindingContext != null)
                    throw new TabbedPagePageShouldntAlreadyHaveBindingContext(this, bindingContext, newPage);
                
                newPage.BindingContext = bindingContext as INotifyPropertyChanged;
                
                var button = new TabbedPageButtonView
                {
                    ButtonPressedCommand = new Command
                    {
                        CanExecute = true,
                        ExecuteAction = () =>
                        {
                            SelectedPage = bindingContext;
                            Selected(SelectedPage);
                        }
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
            if (_pages.Count == 0)
                return;
            
            TabbedButtonSelect(bindingContext);
            if (bindingContext == null)
            {
                _tabbedPageContent.Content = _pages.First();
                return;
            }

            var selectedEntry = _pages.FirstOrDefault(o => Equals(o.BindingContext, bindingContext));

            if (selectedEntry == null)
                return;
            
            _tabbedPageContent.Content = selectedEntry;
        }

        private void TabbedButtonSelect(object bindingContext)
        {
            if (ItemSource == null || ItemSource.Count == 0)
                return;
            
            var index = bindingContext == null ? 0 : ItemSource.IndexOf(bindingContext);

            if (index == -1)
                return;
            
            foreach (var item in _tabSelect.Children)
                ((TabbedPageButtonView) item).IsSelected = false;
            
            ((TabbedPageButtonView)_tabSelect.Children[index]).IsSelected = true;
        }

        public override void SetStyleDictionary(IStyleDictionary styleDictionary)
        {
            base.SetStyleDictionary(styleDictionary);

            foreach (var page in _pages)
                page.SetStyleDictionary(styleDictionary);

            foreach (var layoutable in _tabSelect.Children)
                (layoutable as View)?.SetStyleDictionary(styleDictionary);
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == SelectedPageProperty.PropertyName)
                Selected(SelectedPage);
        }
    }
}