using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Unity.Editor.DataBinding;

namespace WellFired.Guacamole.Examples.Simple.TwoWayBindingExample
{
	public class TwoWayBindingTestModel : ObservableScriptableObject
	{
		[HideInInspector] [SerializeField] private string _boundText = "Initial Text";

		[ExposeProperty]
		[UsedImplicitly]
		public string BoundText
		{
			get { return _boundText; }
			set { SetProperty(ref _boundText, value); }
		}
	}
}