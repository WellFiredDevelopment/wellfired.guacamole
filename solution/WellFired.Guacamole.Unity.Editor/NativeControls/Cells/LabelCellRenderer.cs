using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(LabelCell), typeof(LabelCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
    public class LabelCellRenderer : BaseCellRenderer
    {
        private GUIStyle Style { get; set; }

        public override UISize? NativeSize
        {
            get
            {
                var labelCell = Control as LabelCell;
                Debug.Assert(labelCell != null, $"{nameof(labelCell)} != null");

                CreateStyleWith(labelCell);
                return Style.CalcSize(new GUIContent(labelCell.Text)).ToUISize();
            }
        }

        private void CreateStyleWith([NotNull] LabelCell label)
        {
            if (Style == null)
                Style = new GUIStyle();

            Style.focused.background = BackgroundTexture;
            Style.active.background = BackgroundTexture;
            Style.hover.background = BackgroundTexture;
            Style.normal.background = BackgroundTexture;

            Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);

            Style.focused.textColor = label.TextColor.ToUnityColor();
            Style.active.textColor = label.TextColor.ToUnityColor();
            Style.hover.textColor = label.TextColor.ToUnityColor();
            Style.normal.textColor = label.TextColor.ToUnityColor();

            Style.padding = label.Padding.ToRectOffset();
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);

            var textCell = Control as LabelCell;

            Debug.Assert(textCell != null, "label != null");

            CreateStyleWith(textCell);

            var offset = (float) Control.CornerRadius;
            var smallest = (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width*0.5f, renderRect.Height*0.5f)) + 0.5f);
            smallest = Mathf.Max(smallest, 2);
            Style.border = new RectOffset(smallest, smallest, smallest, smallest);

            EditorGUI.LabelField(renderRect.ToUnityRect(), textCell.Text, Style);
        }

        public override void ResetStyle()
        {
            Style = null;
        }
    }
}