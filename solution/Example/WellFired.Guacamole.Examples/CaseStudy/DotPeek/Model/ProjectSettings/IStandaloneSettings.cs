namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.ProjectSettings
{
	public interface IStandaloneSettings
	{	
		#region Rendering

		ColorSpace ColorSpace { get; set; }
		bool AutoGraphicsApiWin { get; set; }
		bool AutoGraphicsApiMac { get; set; }
		bool AutoGraphicsApiLinux { get; set; }
		GraphicApi[] WindowsApIs { get; set; }
		GraphicApi[] LinuxApIs { get; set; }
		GraphicApi[] MacOsapIs { get; set; }
		bool StaticBatching { get; set; }
		bool DynamicBatching { get; set; }
		bool GpuSkinning { get; set; }
		bool GraphicsJobs { get; set; }

		#endregion

		#region Configuration

		ScriptingRuntimeVersion ScriptingRuntimeVersion { get; set; }
		ScriptingBackend ScriptingBackend { get; set; }
		ApiCompatibilityLevel ApiCompatibilityLevel { get; set; }

		#endregion
		
		#region Optimization
		bool PrebakeCollisionMeshes { get; set; }
		#endregion
	}
}