using System;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	public class PreprocessorCell : ObservableBase
	{
		public enum PreprocessorStatus
		{
			WasThere,
			New,
			Removed
		}
        
		private string _preProcessor;
		private UIColor _preProcessorBackgroundColor;

		[PublicAPI]
		public string PreProcessor
		{
			get => _preProcessor;
			set => SetProperty(ref _preProcessor, value);
		}
        
		[PublicAPI]
		public UIColor PreProcessorBackgroundColor
		{
			get => _preProcessorBackgroundColor;
			set => SetProperty(ref _preProcessorBackgroundColor, value);
		}

		public PreprocessorCell(string preProcessor, PreprocessorStatus status)
		{
			PreProcessor = preProcessor;
            
			switch (status)
			{
				case PreprocessorStatus.WasThere:
					PreProcessorBackgroundColor = UIColor.FromRGB(40, 40, 40);
					break;
				case PreprocessorStatus.New:
					PreProcessorBackgroundColor = UIColor.FromRGB(0, 136, 43);
					break;
				case PreprocessorStatus.Removed:
					PreProcessorBackgroundColor = UIColor.FromRGB(136, 0, 43);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
	}
}