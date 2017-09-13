using System;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.ProjectSettings
{
    public class PreprocessorCellVM : ObservableBase
    {
        public enum PreprocessorStatus
        {
            WasThere,
            New,
            Removed
        }
        
        private string _preprocessor;
        private UIColor _preprocessorBackgroundColor;

        [PublicAPI]
        public string Preprocessor
        {
            get { return _preprocessor; }
            set { SetProperty(ref _preprocessor, value); }
        }
        
        [PublicAPI]
        public UIColor PreprocessorBackgroundColor
        {
            get { return _preprocessorBackgroundColor; }
            set { SetProperty(ref _preprocessorBackgroundColor, value); }
        }

        public PreprocessorCellVM(string preprocessor, PreprocessorStatus status)
        {
            Preprocessor = preprocessor;
            
            switch (status)
            {
                case PreprocessorStatus.WasThere:
                    PreprocessorBackgroundColor = UIColor.FromRGB(40, 40, 40);
                    break;
                case PreprocessorStatus.New:
                    PreprocessorBackgroundColor = UIColor.FromRGB(0, 136, 43);
                    break;
                case PreprocessorStatus.Removed:
                    PreprocessorBackgroundColor = UIColor.FromRGB(136, 0, 43);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}