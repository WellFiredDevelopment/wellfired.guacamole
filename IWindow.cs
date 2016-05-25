namespace WellFired.Guacamole
{
	public interface IWindow
	{
		Window MainContent
		{
			get;
		}

		string Title 
		{
			get;
			set;
		}

		UIRect Rect
		{
			get;
			set;
		}

		UISize MinSize
		{
			get;
			set;
		}

		UISize MaxSize
		{
			get;
			set;
		}

		void Launch(IInitializationContext initializationContext);
	}
}