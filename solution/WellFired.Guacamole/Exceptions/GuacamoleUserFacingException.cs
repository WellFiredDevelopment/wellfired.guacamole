using System;

namespace WellFired.Guacamole.Exceptions
{
    public abstract class GuacamoleUserFacingException : Exception
    {
        public abstract string UserFacingError();
    }
}