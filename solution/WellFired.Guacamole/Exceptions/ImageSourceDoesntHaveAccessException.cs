namespace WellFired.Guacamole.Exceptions
{
    public class ImageSourceDoesntHaveAccessException : GuacamoleUserFacingException
    {
        private readonly string _sourceFilename;

        public ImageSourceDoesntHaveAccessException(string sourceFilename)
        {
            _sourceFilename = sourceFilename;
        }

        public override string UserFacingError()
        {
            return $"The filename passed to this ImageSource does not contain a valid image, or you don't have access rights to this location. Please double check the filename is correct. FileName : {_sourceFilename}";
        }
    }
}