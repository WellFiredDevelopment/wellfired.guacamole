using System.ComponentModel;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ImageView), typeof(ImageViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class ImageViewRenderer : BaseRenderer
    {
        private Texture _texture;
        private readonly ImageLoader _imageLoader = new ImageLoader();

        public override UISize? NativeSize => _texture == null ? UISize.Zero : Style.CalcSize(new GUIContent(_texture)).ToUISize();

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
			
            EditorGUI.LabelField(UnityRect, "", Style);
            
            if(_texture != null)
                GUI.DrawTexture(UnityRect, _texture, ScaleMode.ScaleToFit);
        }

        public override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnViewPropertyChanged(sender, e);

            if (e.PropertyName != ImageView.ImageSourceProperty.PropertyName)
                return;

            TaskEx.Run(() => UpdateTexture());
        }
        
        private async void UpdateTexture()
        {
            var imageView = (ImageView) Control;

            var imageSource = imageView.ImageSource;
            var texture = await _imageLoader.LoadImage(imageSource, () => imageView.ImageSource == imageSource);
			
            if (texture == default(Texture2D)) 
                return;
				
            _texture = texture;
        }
    }
}