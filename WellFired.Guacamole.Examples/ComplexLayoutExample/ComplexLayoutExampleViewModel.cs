using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole.Examples.ComplexLayoutExample
{
	[UsedImplicitly]
	public class ComplexLayoutExampleViewModel : ObservableBase
	{
	    private ComplexLayoutExampleModel Model { get; set; }

		public float CurrentSequenceDuration
		{
			get { return Model.CurrentSequenceDuration; }
			set 
			{ 
				var data = Model.CurrentSequenceDuration;
				if(SetProperty (ref data, value, "CurrentSequenceDuration"))
					Model.CurrentSequenceDuration = data;
			}
		}

		public string CurrentSequenceName
		{
			get { return Model.CurrentSequenceName; }
			set 
			{ 
				var data = Model.CurrentSequenceName;
				if(SetProperty (ref data, value, "CurrentSequenceName"))
					Model.CurrentSequenceName = data;
			}
		}
	}
}