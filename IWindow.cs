namespace WellFired.Guacamole
{
	public interface IWindow
	{
		Window MainContent
		{
			get;
			set;
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
	}
}