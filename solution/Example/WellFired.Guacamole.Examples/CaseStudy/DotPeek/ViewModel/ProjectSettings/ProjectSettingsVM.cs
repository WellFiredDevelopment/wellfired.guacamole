using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.ProjectSettings
{
    public class ProjectSettingsVM : ObservableBase
    {
        private List<PreprocessorCellVM> _preprocessorList;

        [PublicAPI]
        public List<PreprocessorCellVM> PreprocessorsList
        {
            get { return _preprocessorList; }
            set { SetProperty(ref _preprocessorList, value); }
        }

        public ProjectSettingsVM(BuildReport buildReport, BuildReport previousBuildReport = null)
        {
            var preprocessorCellVMList = new List<PreprocessorCellVM>();
            
            if (previousBuildReport != null)
            {
                preprocessorCellVMList.AddRange(from preprocessor in buildReport.Preprocessors
                    let newlyAdded = !previousBuildReport.Preprocessors.Contains(preprocessor)
                    select new PreprocessorCellVM(preprocessor, newlyAdded
                        ? PreprocessorCellVM.PreprocessorStatus.New
                        : PreprocessorCellVM.PreprocessorStatus.WasThere));

                preprocessorCellVMList.AddRange(from preprocessor in previousBuildReport.Preprocessors 
                    let wasRemoved = !buildReport.Preprocessors.Contains(preprocessor) 
                    where wasRemoved 
                    select new PreprocessorCellVM(preprocessor, PreprocessorCellVM.PreprocessorStatus.Removed));
            }
            else
            {
                preprocessorCellVMList.AddRange(
                    buildReport.Preprocessors.Select(
                        preprocessor => new PreprocessorCellVM(preprocessor, PreprocessorCellVM.PreprocessorStatus.WasThere)
                    )
                );
            }

            PreprocessorsList = preprocessorCellVMList.ToList();
        }
    }
}