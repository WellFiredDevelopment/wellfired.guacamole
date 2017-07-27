using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(ImageCell), typeof(ImageCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
    public class ImageCellRenderer : BaseCellRenderer
    {
        private Texture _texture;
        private IImageSource _subscribedImageSource;

        public override UISize? NativeSize
        {
            get
            {
                var imageCell = Control as ImageCell;
                Debug.Assert(imageCell != null, $"{nameof(imageCell)} != null");
                return _texture == null ? UISize.Zero : Style.CalcSize(new GUIContent(_texture)).ToUISize();
            }
        }

        public override void Create()
        {
            base.Create();
			
            var imageCell = (ImageCell)Control;
            if (imageCell.ImageSource != null)
                imageCell.ImageSource.OnComplete += OnLoadComplete;
        }

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
			
            EditorGUI.LabelField(UnityRect, "", Style);
            
            if(_texture != null)
                GUI.DrawTexture(UnityRect, _texture, ScaleMode.ScaleToFit);
        }

        public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            var imageCell = (ImageCell)Control;
			
            if (e.PropertyName != ImageCell.ImageSourceProperty.PropertyName) 
                return;

            if (imageCell.ImageSource == null) 
                return;
            
            _texture = null;
            if(_subscribedImageSource != null)
                _subscribedImageSource.OnComplete -= OnLoadComplete;
            _subscribedImageSource = imageCell.ImageSource; 
            imageCell.ImageSource.OnComplete += OnLoadComplete;
            
            try
            {
                imageCell.ImageSource.Load();
            }
            catch (Exception ex)
            {
                Device.ExecuteOnMainThread(() => { throw ex; });
            }
        }

        private async void OnLoadComplete(LoadedImage image)
        {
            var imageCell = (ImageCell)Control;
            _texture = await imageCell.ImageSource.ToUnityTexture(image);
            Control.InvalidateRectRequest();
        }
    }
}