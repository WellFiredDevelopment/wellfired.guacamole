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
        private Texture2D _texture;
        private readonly ImageLoader _imageLoader = new ImageLoader();

        private bool _isNineSlice;
        private bool _instantiateNineSliceData;
        private GUIStyle _nineSliceStyle;
        private UIPadding _nineSliceRect;

        public override UISize? NativeSize => _texture == null ? UISize.Zero : Style.CalcSize(new GUIContent(_texture)).ToUISize();

        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
			
            EditorGUI.LabelField(UnityRect, "", Style);

            if (_texture == null) 
                return;

            if (_isNineSlice)
            {
                // Instantiation must happen in Unity's UI thread, so we need to postpone this from the load ASync process
                if (_instantiateNineSliceData)
                {
                    _instantiateNineSliceData = false;
                    _nineSliceStyle = new GUIStyle(Style) {
                        focused = {background = _texture},
                        active = {background = _texture},
                        normal = {background = _texture},
                        hover = {background = _texture},
                        border = new RectOffset(
                            _nineSliceRect.Left,
                            _nineSliceRect.Top,
                            _nineSliceRect.Right,
                            _nineSliceRect.Bottom)
                    };
                }
                
                EditorGUI.LabelField(UnityRect, "", _nineSliceStyle);
            }
            else
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
            _isNineSlice = false;

            var nineSliceDefinition = imageSource.NineSliceDefinition;
            if (!nineSliceDefinition.HasValue) 
                return;

            _isNineSlice = true;
            _instantiateNineSliceData = true;
            _nineSliceRect = nineSliceDefinition.Value;
        }
    }
}