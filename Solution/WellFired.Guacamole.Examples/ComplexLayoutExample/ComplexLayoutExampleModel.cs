using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Unity.Editor.DataBinding;

namespace WellFired.Guacamole.Examples.ComplexLayoutExample
{
	[UsedImplicitly]
	public class ComplexLayoutExampleModel : ObservableScriptableObject
	{
		[SerializeField] private float _currentSequenceDuration;

		[SerializeField] private string _currentSequenceName;

		public float CurrentSequenceDuration
		{
			get { return _currentSequenceDuration; }
			set { SetProperty(ref _currentSequenceDuration, value, "CurrentSequenceDuration"); }
		}

		public string CurrentSequenceName
		{
			get { return _currentSequenceName; }
			set { SetProperty(ref _currentSequenceName, value, "CurrentSequenceName"); }
		}
	}
}