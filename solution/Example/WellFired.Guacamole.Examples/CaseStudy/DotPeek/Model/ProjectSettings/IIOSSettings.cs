namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.ProjectSettings
{
	public interface IIosSettings
	{
		#region Debugging And Crash Reporting
		OnUnhandledException UnhandledException { get; set; }
		bool LogObjCException { get; set; }
		bool EnableCrashReport { get; set; }
		#endregion

		#region Rendering
		ColorSpace ColorSpace { get; set; }
		bool AutoGraphicsApi { get; set; }
		GraphicApi[] ApIs { get; set; }
		bool ForceHardShadowOnMetal { get; set; }
		bool StaticBatching { get; set; }
		bool DynamicBatching { get; set; }
		bool GpuSkinning { get; set; }
		bool GraphicsJobs { get; set; }
		#endregion

		#region Identification
		string BundleIdentifier { get; set; }
		string Version { get; set; }
		string Build { get; set; }
		bool AutomaticallySign { get; set; }
		#endregion

		#region Configuration
		ScriptingRuntimeVersion ScriptingRuntimeVersion { get; set; }
		ScriptingBackend ScriptingBackend { get; set; }
		ApiCompatibilityLevel ApiCompatibilityLevel { get; set; }
		TargetDevice TargetDevice { get; set; }
		string TargetMinimumSdk { get; set; }
		bool RequiresPersistentWifi { get; set; }
		bool AllowDownloadsOverHttp { get; set; }
		Architecture Architecture { get; set; }
		#endregion

		#region Optimization
		bool PrebakeCollisionMeshes { get; set; }
		string AotCompilationOptions { get; set; }
		bool StripEngineCode { get; set; }
		ScriptCallOptimization ScriptCallOptimization { get; set; }
		#endregion
	}
}