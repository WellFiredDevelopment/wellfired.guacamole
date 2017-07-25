using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;

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

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();
			
			var imageView = (ImageView)Control;
			if(imageView.ImageSource != null)
				_texture = imageView.ImageSource.BuildTexture();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			
			GUI.DrawTexture(UnityRect, _texture, ScaleMode.ScaleToFit);
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			var imageView = (ImageView)Control;
			if (e.PropertyName == ImageView.ImageSourceProperty.PropertyName)
			{
				_texture = imageView.ImageSource.BuildTexture();
				imageView.InvalidateRectRequest();
			}
		}
	}
}