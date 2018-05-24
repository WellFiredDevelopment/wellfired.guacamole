using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;

[assembly: CustomRenderer(typeof(LabelCell), typeof(LabelCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
    public class LabelCellRenderer : BaseCellRenderer
    {
        public override UISize? NativeSize
        {
            get
            {
                var labelCell = Control as LabelCell;
                // ReSharper disable once PossibleNullReferenceException
                return Style.CalcSize(new GUIContent(labelCell.Text)).ToUISize();
            }
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);

            var textCell = (LabelCell)Control;
            EditorGUI.LabelField(UnityRect, textCell.Text, Style);
        }

        protected override void SetupWithNewStyle()
        {
            base.SetupWithNewStyle();

            var labelCell = (LabelCell)Control;
            Style.alignment = UITextAlignExtensions.Combine(labelCell.HorizontalTextAlign, labelCell.VerticalTextAlign);
            Style.focused.textColor = labelCell.TextColor.ToUnityColor();
            Style.active.textColor = labelCell.TextColor.ToUnityColor();
            Style.hover.textColor = labelCell.TextColor.ToUnityColor();
            Style.normal.textColor = labelCell.TextColor.ToUnityColor();
            Style.fontSize = labelCell.FontSize;
        }

        public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            var labelCell = (LabelCell)Control;
            if (e.PropertyName == LabelCell.TextColorProperty.PropertyName)
            {
                Style.focused.textColor = labelCell.TextColor.ToUnityColor();
                Style.active.textColor = labelCell.TextColor.ToUnityColor();
                Style.hover.textColor = labelCell.TextColor.ToUnityColor();
                Style.normal.textColor = labelCell.TextColor.ToUnityColor();
            }
			
            if(e.PropertyName == LabelCell.HorizontalTextAlignProperty.PropertyName || e.PropertyName == LabelCell.VerticalTextAlignProperty.PropertyName)
                Style.alignment = UITextAlignExtensions.Combine(labelCell.HorizontalTextAlign, labelCell.VerticalTextAlign);
        }
    }
}