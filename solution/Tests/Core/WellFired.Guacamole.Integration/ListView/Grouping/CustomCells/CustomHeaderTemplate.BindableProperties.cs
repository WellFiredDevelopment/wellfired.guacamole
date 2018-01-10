using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.ListView.Grouping.CustomCells
{
    public partial class CustomHeaderTemplate
    {
        [PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<CustomEntryTemplate, string>(
            string.Empty,
            BindingMode.TwoWay,
            cell => cell.Text
        );

        [PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty.Create<CustomEntryTemplate, UIColor>(
            UIColor.Black,
            BindingMode.TwoWay,
            cell => cell.TextColor
        );

        [PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
            .Create<CustomEntryTemplate, UITextAlign>(
                UITextAlign.Start,
                BindingMode.TwoWay,
                cell => cell.HorizontalTextAlign
            );

        [PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
            .Create<CustomEntryTemplate, UITextAlign>(
                UITextAlign.Middle,
                BindingMode.TwoWay,
                cell => cell.VerticalTextAlign
            );

        [PublicAPI]
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        [PublicAPI]
        public UIColor TextColor
        {
            get => (UIColor) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        [PublicAPI]
        public UITextAlign HorizontalTextAlign
        {
            get => (UITextAlign) GetValue(HorizontalTextAlignProperty);
            set => SetValue(HorizontalTextAlignProperty, value);
        }

        [PublicAPI]
        public UITextAlign VerticalTextAlign
        {
            get => (UITextAlign) GetValue(VerticalTextAlignProperty);
            set => SetValue(VerticalTextAlignProperty, value);
        }
    }
}