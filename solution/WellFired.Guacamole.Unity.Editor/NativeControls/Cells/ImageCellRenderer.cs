using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(ImageCell), typeof(ImageCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
    public class ImageCellRenderer : BaseCellRenderer
    {
        private Texture _texture;
        private readonly ImageCreatorHandler _handler = new ImageCreatorHandler();

        public override UISize? NativeSize
        {
            get
            {
                var imageCell = Control as ImageCell;
                Debug.Assert(imageCell != null, $"{nameof(imageCell)} != null");
                return _texture == null ? UISize.Zero : Style.CalcSize(new GUIContent(_texture)).ToUISize();
            }
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
			
            EditorGUI.LabelField(UnityRect, "", Style);
            
            if(_texture != null)
                GUI.DrawTexture(UnityRect, _texture, ScaleMode.ScaleToFit);
        }

        public override async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            var imageCell = (ImageCell) Control;
            if (e.PropertyName != ImageCell.ImageSourceProperty.PropertyName)
                return;

            _texture = null;
            _texture = await _handler.UpdatedImageSource(imageCell.ImageSource);
        }
    }
}