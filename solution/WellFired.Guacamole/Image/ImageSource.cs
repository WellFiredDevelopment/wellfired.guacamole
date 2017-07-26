using System;
using System.IO;
using System.Threading;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Image
{
    public class ImageSource : IImageSource
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ISourceHandler _handler;
        private LoadedImage _loadedImage;

        public bool InProgress => _cancellationTokenSource != default(CancellationTokenSource);
        public Action<LoadedImage> OnComplete { get; set; } = delegate {};

        private ImageSource(string location)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new FileSourceHandler(location);
        }

        private ImageSource(Uri location)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new UriSourceHandler(location);
        }

        private ImageSource(Stream stream)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new StreamSourceHandler(stream);
        }

        private ImageSource(ImageShapeDefinition imageShapeDefinition)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new ImageShapeDefinitionHandler(imageShapeDefinition);
        }

        public async void Load()
        {
            // We've already loaded, return immediately and call complete.
            if (_loadedImage != default(LoadedImage))
            {
                DoEnd();
                return;
            }
            
            // we're already loading, so return immediately.
            if (_cancellationTokenSource == default(CancellationTokenSource))
                return;

            IImageSourceWrapper wrapper;
            try
            {
                wrapper = await _handler.Handle(_cancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                Device.ExecuteOnMainThread(() => { throw e; });
                return;
            }
            
            if (wrapper == null)
            {
                _cancellationTokenSource = default(CancellationTokenSource);
                return;
            }
            
            End(wrapper);
            _cancellationTokenSource = default(CancellationTokenSource);
        }

        public void Cancel()
        {
            // We can cancel our async tasks at any time.
            if(_cancellationTokenSource != default(CancellationTokenSource))
                _cancellationTokenSource.Cancel();
        }

        private void End(IImageSourceWrapper imageSourceWrapper)
        {
            _loadedImage = LoadedImage.From(imageSourceWrapper);
            DoEnd();
        }

        private void DoEnd()
        {
            try
            {
                OnComplete(_loadedImage);
            }
            catch (Exception e)
            {
                Device.ExecuteOnMainThread(() => { throw e; });
            }
        }

        /// <summary>
        /// The image passed should be a per platform image location, see the documentation for your desired platform for more information.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static IImageSource From(string location)
        {
            return new ImageSource(location);
        }

        /// <summary>
        /// Here you can pass a URI to load an image from. Any URI should be valid.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static IImageSource From(Uri location)
        {
            return new ImageSource(location);
        }

        /// <summary>
        /// Load an image from a stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IImageSource From(Stream stream)
        {
            return new ImageSource(stream);
        }

        /// <summary>
        /// Loads an Image from a shape definition
        /// </summary>
        /// <param name="imageShape"></param>
        /// <param name="color"></param>
        /// <param name="outlineColor"></param>
        /// <returns></returns>
        public static IImageSource From(ImageShape imageShape, UIColor color, UIColor outlineColor)
        {
            return new ImageSource(new ImageShapeDefinition { Shape = imageShape, Size = 64, Color = color, OutlineColor = outlineColor });
        }
    }
}