using WellFired.Guacamole.Exceptions;

namespace WellFired.Guacamole.Unity.Editor.Exception
{
    public class ImageSourceCouldntFindFileException : GuacamoleUserFacingException
    {
        private readonly string _sourceFilename;

        public ImageSourceCouldntFindFileException(string sourceFilename)
        {
            _sourceFilename = sourceFilename;
        }

        public override string UserFacingError()
        {
            return $"The filename passed to this ImageSource does not contain a valid image. Please double check the filename is correct. FileName : {_sourceFilename}";
        }
    }
}