﻿using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Cells
{
    public partial class LabelCell
    {
        [PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<LabelCell, string>(
            string.Empty,
            BindingMode.TwoWay,
            cell => cell.Text
        );

        [PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty.Create<LabelCell, UIColor>(
            UIColor.Black,
            BindingMode.TwoWay,
            cell => cell.TextColor
        );

        [PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
            .Create<LabelCell, UITextAlign>(
                UITextAlign.Start,
                BindingMode.TwoWay,
                cell => cell.HorizontalTextAlign
            );

        [PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
            .Create<LabelCell, UITextAlign>(
                UITextAlign.Middle,
                BindingMode.TwoWay,
                cell => cell.VerticalTextAlign
            );

        [PublicAPI]
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        [PublicAPI]
        public UIColor TextColor
        {
            get { return (UIColor) GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        [PublicAPI]
        public UITextAlign HorizontalTextAlign
        {
            get { return (UITextAlign) GetValue(HorizontalTextAlignProperty); }
            set { SetValue(HorizontalTextAlignProperty, value); }
        }

        [PublicAPI]
        public UITextAlign VerticalTextAlign
        {
            get { return (UITextAlign) GetValue(VerticalTextAlignProperty); }
            set { SetValue(VerticalTextAlignProperty, value); }
        }
    }
}