namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.ProjectSettings
{
	public interface IAndroidSettings
	{
		#region Rendering
		ColorSpace ColorSpace { get; set; }
		bool AutoGraphicsApi { get; set; }
		GraphicApi[] ApIs { get; set; }
		bool MultithreadedRendering { get; set; }
		bool StaticBatching { get; set; }
		bool DynamicBatching { get; set; }
		bool GpuSkinning { get; set; }
		bool GraphicsJobs { get; set; }
		#endregion

		#region Identification
		string PackageName { get; set; }
		string Version { get; set; }
		int BundleVersionCode { get; set; }
		int MinimumApiLevelSupported { get; set; }
		int TargetApiLevel { get; set; }
		#endregion

		#region Configuration
		ScriptingRuntimeVersion ScriptingRuntimeVersion { get; set; }
		ScriptingBackend ScriptingBackend { get; set; }
		ApiCompatibilityLevel ApiCompatibilityLevel { get; set; }
		DeviceFilter DeviceFilter { get; set; }
		InstallationLocation InstallationLocation { get; set; }
		WritePermission WritePermission { get; set; }
		#endregion

		#region Optimization
		bool PrebakeCollisionMeshes { get; set; }
		StrippingLevel StrippingLevel { get; set; }
		bool SplitApplicationBinary { get; set; }
		#endregion

		#region Publishing Settings
		string KeyAlias { get; set; }
		#endregion
	}
}