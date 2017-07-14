using System;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Examples.Unity.Editor.NativeControls.Cells;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(FilterCell), typeof(FilterCellRenderer))]
namespace WellFired.Guacamole.Examples.Unity.Editor.NativeControls.Cells
{
    public class FilterCellRenderer : BaseRenderer
    {
        private const int Spacing = 16;
        private const int Offset = 10;
        private UISize _ballSize = UISize.Of(12);
        private GUIStyle Style { get; set; }
        private Texture2D CircleTexture { get; set; }

        public override UISize? NativeSize
        {
            get
            {
                var filterCell = Control as FilterCell;
                Debug.Assert(filterCell != null, $"{nameof(filterCell)} != null");

                CreateStyleWith(filterCell);
                var size = Style.CalcSize(new GUIContent(filterCell.Text)).ToUISize();

                size.Width += _ballSize.Height;
                size.Width += Spacing;
                size.Height = Math.Max(size.Height, _ballSize.Height);
                    
                return size;
            }
        }

        private void CreateStyleWith([NotNull] FilterCell label)
        {
            if (Style == null)
                Style = new GUIStyle();
            
            if(CircleTexture == null)
                CircleTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, label.FilterColor, label.FilterColor, 32.0f, CornerMask.All);

            Style.focused.background = BackgroundTexture;
            Style.active.background = BackgroundTexture;
            Style.hover.background = BackgroundTexture;
            Style.normal.background = BackgroundTexture;

            Style.alignment = UITextAlignExtensions.Combine(UITextAlign.Start, UITextAlign.Middle);

            Style.focused.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.active.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.hover.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.normal.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();

            Style.fontSize = 14;
            Style.fontStyle = FontStyle.Normal;

            Style.padding = label.Padding.ToRectOffset();
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);

            var filterCell = Control as FilterCell;

            Debug.Assert(filterCell != null, "label != null");

            CreateStyleWith(filterCell);

            var offset = (float) Control.CornerRadius;
            var smallest = (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width*0.5f, renderRect.Height*0.5f)) + 0.5f);
            smallest = Mathf.Max(smallest, 2);
            Style.border = new RectOffset(smallest, smallest, smallest, smallest);     
            
            EditorGUI.LabelField(renderRect.ToUnityRect(), "", Style);

            var ballRect = renderRect;
            ballRect.X += Offset;
            ballRect.Width = _ballSize.Width;
            ballRect.Height = _ballSize.Height;
            ballRect.Y += (renderRect.Height - _ballSize.Height) / 2;
            GUI.DrawTexture(ballRect.ToUnityRect(), CircleTexture);

            var textRect = renderRect;
            textRect.X += ballRect.Width + Spacing + Offset;
            textRect.Width -= ballRect.Width;
            textRect.Width -= Spacing;
            textRect.Width -= Offset;
            EditorGUI.LabelField(textRect.ToUnityRect(), filterCell.Text, Style);
        }

        public override void ResetStyle()
        {
            Style = null;
        }
    }
}