using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.FileSystem;
using WellFired.Guacamole.WebRequestHandler;

namespace WellFired.Guacamole.Image
{
    public sealed class ImageSource : IImageSource
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ISourceHandler _handler;
        private LoadedImage _loadedImage;

        private readonly Semaphore _loadingSemaphore = new Semaphore(1, 1);

        public Action<LoadedImage> OnComplete { get; set; } = delegate {};

        private ImageSource(string location, IFileSystem fileSystem)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new FileSourceHandler(location, fileSystem);
        }

        private ImageSource(Uri location, IWebRequestHandler webRequestHandler)
        {   
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new UriSourceHandler(location, webRequestHandler);
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

        /// <summary>
        /// Load the image. If loading is cancelled, then the task will most probably returns a null value when cancellation
        /// finished.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ImageSourceHandlerProducedNoWrapperException"></exception>
        public async Task<LoadedImage> Load()
        {
            try
            {
                _loadingSemaphore.WaitOne();
                
                if (_loadedImage != null)
                    return _loadedImage;

                var wrapper = await _handler.Handle(_cancellationTokenSource.Token);
            
                if (wrapper == null)
                {
                    throw new ImageSourceHandlerProducedNoWrapperException();
                }
            
                _loadedImage = LoadedImage.From(wrapper);
                
            
                return _loadedImage; 
            }
            finally
            {
                _loadingSemaphore.Release();
            }
        }

        /// <summary>
        /// Cancel the current loading process. We can cancel our async tasks at any time, but when it is cancelled exactly the task depends on how the different
        /// handlers handle the cancellation token.
        /// </summary>
        public void Cancel()
        {
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
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        public static IImageSource From(string location, IFileSystem fileSystem = default(IFileSystem))
        {
            if (fileSystem == default(IFileSystem))
                fileSystem = new FileSystem.FileSystem();
            
            return new ImageSource(location, fileSystem);
        }

        /// <summary>
        /// Here you can pass a URI to load an image from. Any URI should be valid.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="webRequestHandler"></param>
        /// <returns></returns>
        public static IImageSource From(Uri location, IWebRequestHandler webRequestHandler = default(IWebRequestHandler))
        {
            if(webRequestHandler == default(IWebRequestHandler))
                webRequestHandler = new WebRequestHandler.WebRequestHandler();
            
            return new ImageSource(location, webRequestHandler);
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
        /// <param name="thickness"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static IImageSource From(ImageShape imageShape, double thickness, UIColor color)
        {
            return new ImageSource(new ImageShapeDefinition { Shape = imageShape, Size = 64, Color = color, OutlineColor = color, Thickness = thickness });
        }

        /// <summary>
        /// Loads an Image from a shape definition
        /// </summary>
        /// <param name="imageShape"></param>
        /// <param name="thickness"></param>
        /// <param name="color"></param>
        /// <param name="outlineColor"></param>
        /// <returns></returns>
        public static IImageSource From(ImageShape imageShape, double thickness, UIColor color, UIColor outlineColor)
        {
            return new ImageSource(new ImageShapeDefinition { Shape = imageShape, Size = 64, Color = color, OutlineColor = outlineColor, Thickness = thickness });
        }
    }
}