using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;

[assembly: CustomRenderer(typeof(HeaderCell), typeof(HeaderCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
    public class HeaderCellRenderer : BaseCellRenderer
    {
        protected override bool CanMouseOver { get; } = false;

        public override UISize? NativeSize
        {
            get
            {
                var headerCell = Control as HeaderCell;
                // ReSharper disable once PossibleNullReferenceException
                return Style.CalcSize(new GUIContent(headerCell.Text)).ToUISize();
            }
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);

            var headerCell = (HeaderCell)Control;
            EditorGUI.LabelField(UnityRect, headerCell.Text, Style);
        }

        protected override void SetupWithNewStyle()
        {
            base.SetupWithNewStyle();

            var headerCell = (HeaderCell)Control;
            Style.alignment = UITextAlignExtensions.Combine(headerCell.HorizontalTextAlign, headerCell.VerticalTextAlign);
            Style.focused.textColor = headerCell.TextColor.ToUnityColor();
            Style.active.textColor = headerCell.TextColor.ToUnityColor();
            Style.hover.textColor = headerCell.TextColor.ToUnityColor();
            Style.normal.textColor = headerCell.TextColor.ToUnityColor();
        }

        public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            var headerCell = (HeaderCell)Control;
            if (e.PropertyName == HeaderCell.TextColorProperty.PropertyName)
            {
                Style.focused.textColor = headerCell.TextColor.ToUnityColor();
                Style.active.textColor = headerCell.TextColor.ToUnityColor();
                Style.hover.textColor = headerCell.TextColor.ToUnityColor();
                Style.normal.textColor = headerCell.TextColor.ToUnityColor();
            }
			
            if(e.PropertyName == HeaderCell.HorizontalTextAlignProperty.PropertyName || e.PropertyName == HeaderCell.VerticalTextAlignProperty.PropertyName)
                Style.alignment = UITextAlignExtensions.Combine(headerCell.HorizontalTextAlign, headerCell.VerticalTextAlign);
        }
    }
}