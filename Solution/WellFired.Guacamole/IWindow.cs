using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.InitializationContext;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole
{
	public interface IWindow
    {
        [PublicAPI]
        Window MainContent
		{
			get;
		}

        [PublicAPI]
        string Title 
		{
			get;
			set;
		}

        [PublicAPI]
        UIRect Rect
		{
			get;
			set;
		}

        [PublicAPI]
        UISize MinSize
		{
			get;
			set;
		}

        [PublicAPI]
        UISize MaxSize
		{
			get;
			set;
		}

        [PublicAPI]
        void Launch(IInitializationContext initializationContext);
	}
}