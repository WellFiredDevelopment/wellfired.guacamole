using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	public class ProjectSettings : ObservableBase
	{
		private List<PreprocessorCell> _preProcessorList;

		[PublicAPI]
		public List<PreprocessorCell> PreProcessorsList
		{
			get => _preProcessorList;
			set => SetProperty(ref _preProcessorList, value);
		}

		public ProjectSettings(BuildReport buildReport, BuildReport previousBuildReport)
		{
			if (previousBuildReport != BuildReport.Null)
			{
				var preprocessorCellVMList = new List<PreprocessorCell>();

				preprocessorCellVMList.AddRange(from preprocessor in buildReport.BuildSettings.CompileDirectives
					let newlyAdded = !previousBuildReport.BuildSettings.CompileDirectives.Contains(preprocessor)
					select new PreprocessorCell(preprocessor, newlyAdded
						? PreprocessorCell.PreprocessorStatus.New
						: PreprocessorCell.PreprocessorStatus.WasThere));

				preprocessorCellVMList.AddRange(from preprocessor in previousBuildReport.BuildSettings.CompileDirectives
					let wasRemoved = !buildReport.BuildSettings.CompileDirectives.Contains(preprocessor)
					where wasRemoved
					select new PreprocessorCell(preprocessor, PreprocessorCell.PreprocessorStatus.Removed));

				PreProcessorsList = preprocessorCellVMList;
				return;
			}

			PreProcessorsList = buildReport.BuildSettings.CompileDirectives.Select(preprocessor => new PreprocessorCell(preprocessor, PreprocessorCell.PreprocessorStatus.WasThere)).ToList();
		}
	}
}