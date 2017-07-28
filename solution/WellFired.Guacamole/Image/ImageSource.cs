using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Image
{
    public class ImageSource : IImageSource
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ISourceHandler _handler;
        private LoadedImage _loadedImage;
        private bool _isLoading;

        public bool InProgress => _cancellationTokenSource != default(CancellationTokenSource);
        public Action<LoadedImage> OnComplete { get; set; } = delegate {};

        private ImageSource(string location)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new FileSourceHandler(location, new FileSystem.FileSystem());
        }

        private ImageSource(Uri location)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new UriSourceHandler(location, new WebRequestHandler.WebRequestHandler());
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

        public async Task<LoadedImage> Load()
        {
            if (_loadedImage != null)
                return _loadedImage;
            
            // we're already loading, so return immediately.
            if (_isLoading)
                throw new AlreadyLoadingException();

            _isLoading = true;
            var wrapper = await _handler.Handle(_cancellationTokenSource.Token);
            
            if (wrapper == null)
            {
                _isLoading = false;
                throw new HandlerProducedNoWrapperException();
            }
            
            _isLoading = false;
            _loadedImage = LoadedImage.From(wrapper);
            return _loadedImage;
        }

        public void Cancel()
        {
            // We can cancel our async tasks at any time.
            _cancellationTokenSource.Cancel();
        }

        public override string ToString()
        {
            return $"{_handler}";
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

    public class HandlerProducedNoWrapperException : Exception
    {
    }

    public class AlreadyLoadingException : Exception
    {
    }
}