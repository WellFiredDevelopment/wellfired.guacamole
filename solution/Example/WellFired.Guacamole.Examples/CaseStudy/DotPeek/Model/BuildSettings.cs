using System.Collections.Generic;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.ProjectSettings;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
	public class BuildSettings
	{
		public IIosSettings IosSettings;
		public IAndroidSettings AndroidSettings;
		public IStandaloneSettings StandaloneSettings;
		
		public PreprocessorOrigin PreprocessorOrigin;
		public List<string> CompileDirectives = new List<string>();
	}
}