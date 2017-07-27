using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;
using ILogger = WellFired.Guacamole.Diagnostics.ILogger;
using Logger = WellFired.Guacamole.Diagnostics.Logger;

[assembly: CustomRenderer(typeof(ImageView), typeof(ImageViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class ImageViewRenderer : BaseRenderer
    {
        private Texture _texture;

        public override UISize? NativeSize
        {
            get
            {
                var imageView = Control as ImageView;
                Debug.Assert(imageView != null, $"{nameof(imageView)} != null");
                return _texture == null ? UISize.Zero : Style.CalcSize(new GUIContent(_texture)).ToUISize();
            }
        }

        public override void Create()
        {
            base.Create();
			
            var imageView = (ImageView)Control;
            if (imageView.ImageSource != null)
                imageView.ImageSource.OnComplete += OnLoadComplete;
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

            var imageView = (ImageView)Control;
			
            if (e.PropertyName != ImageView.ImageSourceProperty.PropertyName) 
                return;

            if (imageView.ImageSource == null) 
                return;
            
            _texture = null;
            imageView.ImageSource.OnComplete += OnLoadComplete;
            imageView.ImageSource.Load();
        }

        private async void OnLoadComplete(LoadedImage image)
        {
            var imageCell = (ImageView)Control;
            _texture = await imageCell.ImageSource.ToUnityTexture(image);
            Control.InvalidateRectRequest();
        }

        public override void RecycleWithNewBindingContext()
        {
            base.RecycleWithNewBindingContext();
            _texture = null;
        }
    }
}