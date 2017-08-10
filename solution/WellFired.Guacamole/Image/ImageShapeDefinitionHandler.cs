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
            var byteArray = new byte[0];

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
                
                byteArray = ImageData.BuildRounded(_imageShapeDefinition.Size, _imageShapeDefinition.Size, _imageShapeDefinition.Color, _imageShapeDefinition.OutlineColor, corner, _imageShapeDefinition.Thickness, CornerMask.All, OutlineMask.All);
            }, cancellationToken);

            return new ImageSourceWrapper(new MemoryStream(byteArray), ImageType.Raw);
        }

        public override string ToString()
        {
            return $"{_imageShapeDefinition.Shape}";
        }
    }
}