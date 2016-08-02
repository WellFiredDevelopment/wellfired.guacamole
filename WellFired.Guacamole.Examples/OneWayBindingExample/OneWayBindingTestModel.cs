﻿using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Databinding.Unity.Runtime;

namespace WellFired.Guacamole.Examples.TwoWayBinding
{
	[UsedImplicitly]
	public class OneWayBindingTestModel : ObservableScriptableObject
	{
		[HideInInspector] [SerializeField] private string _boundText = "Initial Text";

		[ExposeProperty]
		[UsedImplicitly]
		public string BoundText
		{
			get { return _boundText; }
			set { SetProperty(ref _boundText, value, "BoundText"); }
		}
	}
}