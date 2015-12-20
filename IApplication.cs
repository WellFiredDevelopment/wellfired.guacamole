namespace WellFired.Guacamole
{
	public interface IApplication 
	{
		ViewBase MainContent
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