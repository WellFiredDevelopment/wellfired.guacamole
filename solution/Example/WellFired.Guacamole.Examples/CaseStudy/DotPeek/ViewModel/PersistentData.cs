using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	[Serializable]
	public class PersistentData : ObservableScriptableObject
	{
		private static readonly BuildReport LeftReport = Debug.Utility.GenerateTempBuildReport();
		
		[SerializeField] private string _selectedPageName;
		private ObservableBase _selectedPage;

		public IEnumerable<ObservableBase> TabSource { get; } 
			= new List<ObservableBase> {
				new Overview(LeftReport, BuildReport.Null),
				new ProjectSettings(LeftReport, BuildReport.Null),
				new UnusedAssets(Debug.Utility.ListOfAssets(), null),
				new UsedAssets(Debug.Utility.ListOfAssets(), null),
			};

		public ObservableBase SelectedPage
		{
			get
			{
				if(_selectedPage == default(ObservableBase))
					_selectedPage = TabSource.FirstOrDefault(o => o.GetType().FullName == _selectedPageName);
				return _selectedPage;
			}
			set
			{
				var newPageName = value.GetType().FullName;
				var newPage = TabSource.FirstOrDefault(o => o.GetType().FullName == newPageName);

				if (Equals(SelectedPage, newPage))
					return;

				if (SetProperty(ref _selectedPage, value))
					_selectedPageName = SelectedPage.GetType().FullName;
			}
		}
	}
}