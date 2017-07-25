using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Image
{
    public class ImageSource : IImageSource
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly string _filename = string.Empty;
        private ImageShapeDefinition _imageShapeDefinition;

        public bool InProgress => _cancellationTokenSource != default(CancellationTokenSource);
        public Action OnComplete { get; set; } = delegate {};
        public Image Image { get; } = new Image();

        private ImageSource(string location)
        {
            _filename = Device.AdjustPath(location);
            Begin();
        }

        private ImageSource(ImageShapeDefinition imageShapeDefinition)
        {
            _imageShapeDefinition = imageShapeDefinition;
            Begin();
        }

        private void Begin()
        {
            if(_cancellationTokenSource != default(CancellationTokenSource))
                _cancellationTokenSource.Cancel();

            if (_filename != string.Empty)
                LoadFromFile();
        }

        public void Cancel()
        {
            if(_cancellationTokenSource != default(CancellationTokenSource))
                _cancellationTokenSource.Cancel();
        }

        private void LoadFromFile()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Exception cachedException = null;
            TaskEx.Run(() => {
                try
                {
                    Image.From(File.ReadAllBytes(_filename));
                }
                catch (FileNotFoundException)
                {
                    cachedException = new ImageSourceCouldntFindFileException(_filename);
                }
                catch (DirectoryNotFoundException)
                {
                    cachedException = new ImageSourceCouldntFindFileException(_filename);
                }
                catch (UnauthorizedAccessException)
                {
                    cachedException = new ImageSourceDoesntHaveAccessException(_filename);
                }
                catch (Exception e)
                {
                    cachedException = e;
                }
                finally
                {
                    Device.ExecuteOnMainThread(() => {
                        try
                        {
                            if (cachedException != null)
                            {
                                _cancellationTokenSource = default(CancellationTokenSource);  
                                throw cachedException;
                            }
                            OnComplete();
                        }
                        finally
                        {
                            _cancellationTokenSource = default(CancellationTokenSource);   
                        }
                    });
                }
            });
        }

        private void LoadFromDefinition()
        {
            
        }

        /// <summary>
        /// The image passed should be a per platform image location, see the documentation for your desired platform for more information.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static IImageSource FromFile(string location)
        {
            return new ImageSource(location);
        }

        /// <summary>
        /// Loads an Image from a shape definition
        /// </summary>
        /// <param name="imageShape"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static IImageSource FromDefinition(ImageShape imageShape, int size, UIColor color)
        {
            return new ImageSource(new ImageShapeDefinition { Shape = imageShape, Size = size, Color = color });
        }
    }
}