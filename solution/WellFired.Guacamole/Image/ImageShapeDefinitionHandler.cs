using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Image
{
    internal class ImageShapeDefinitionHandler : ISourceHandler
    {
        private readonly ImageShapeDefinition _imageShapeDefinition;

        public ImageShapeDefinitionHandler(ImageShapeDefinition imageShapeDefinition)
        {
            _imageShapeDefinition = imageShapeDefinition;
        }

        public async Task<IImageSourceWrapper> Handle(CancellationToken cancellationToken)
        {
            var imageIncludingChanel = _imageShapeDefinition.Size * _imageShapeDefinition.Size * 4;
            var byteArray = new byte[imageIncludingChanel];

            await TaskEx.Run(() =>
            {
                int corner;

                switch (_imageShapeDefinition.Shape)
                {
                    case ImageShape.Circle:
                        corner = _imageShapeDefinition.Size / 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                var imageData = ImageData.BuildRounded(_imageShapeDefinition.Size, _imageShapeDefinition.Size, _imageShapeDefinition.Color, _imageShapeDefinition.OutlineColor, corner, 1, CornerMask.All, OutlineMask.All);
                
                var counter = 0;
                foreach (var color in imageData)
                {
                    byteArray[counter + 0] = (byte)(color.R * 255);
                    byteArray[counter + 1] = (byte)(color.G * 255);
                    byteArray[counter + 2] = (byte)(color.B * 255);
                    byteArray[counter + 3] = (byte)(color.A * 255);
                
                    counter += 4;
                }
                
            }, cancellationToken);

            return new ImageSourceWrapper(new MemoryStream(byteArray), ImageType.Raw);
        }

        public override string ToString()
        {
            return $"{_imageShapeDefinition.Shape}";
        }
    }
}