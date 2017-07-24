using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Examples.Unity.Editor.NativeControls.Cells;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(FilterCell), typeof(FilterCellRenderer))]
namespace WellFired.Guacamole.Examples.Unity.Editor.NativeControls.Cells
{
    public class FilterCellRenderer : BaseCellRenderer
    {
        private const int Spacing = 16;
        private const int Offset = 10;
        private UISize _ballSize = UISize.Of(12);
        private Texture2D CircleTexture { get; set; }

        public override UISize? NativeSize
        {
            get
            {
                var filterCell = Control as FilterCell;

                Debug.Assert(filterCell != null, "filterCell != null");
                
                var size = Style.CalcSize(new GUIContent(filterCell.Text)).ToUISize();
                size.Width += _ballSize.Height;
                size.Width += Spacing;
                size.Height = filterCell.Container.EntrySize;
                return size;
            }
        }

        protected override void SetupWithNewStyle()
        {
            base.SetupWithNewStyle();

            var filterCell = (FilterCell)Control;
            
            if(CircleTexture == null)
                CircleTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, filterCell.FilterColor, filterCell.FilterColor, 32.0f, CornerMask.All);

            Style.alignment = UITextAlignExtensions.Combine(UITextAlign.Start, UITextAlign.Middle);
            Style.focused.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.active.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.hover.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.normal.textColor = UIColor.FromRGB(62, 62, 62).ToUnityColor();
            Style.fontSize = 14;
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);

            var filterCell = (FilterCell)Control;  
            
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
    }
}