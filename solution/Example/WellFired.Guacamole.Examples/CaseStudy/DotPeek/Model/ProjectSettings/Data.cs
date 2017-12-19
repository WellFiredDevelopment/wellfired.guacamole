using System;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.ProjectSettings
{
	[Flags]
	public enum VertexCompression
	{
		Nothing = 0x0000,
		Position = 1 << 0,
		Normal = 1 << 1,
		Color = 1 << 2,
		Uv0 = 1 << 3,
		Uv1 = 1 << 4,
		Uv2 = 1 << 5,
		Uv3 = 1 << 6,
		Tangent = 1 << 7,
		Everything = ~0
	}

	public enum ScriptCallOptimization
	{
		SlowAndSafe,
		Fast
	}

	public enum Architecture
	{
		ArMv7,
		Arm64,
		Universal
	}

	public enum TargetDevice
	{
		Phone,
		Pad,
		PhoneIPad
	}

	public enum ScriptingBackend
	{
		Mono,
		Il2Cpp
	}

	public enum ApiCompatibilityLevel
	{
		Net2,
		Net2Subset
	}

	public enum ScriptingRuntimeVersion
	{
		Net35,
		Net46
	}

	public enum GraphicApi
	{
		Metal,
		OpenGles3,
		OpenGles2,
		Vulkan,
		Direct3D11,
		Direct3D9,
		Direct3D12,
		OpenGlCore
	}

	public enum ColorSpace
	{
		Gamma,
		Linear
	}

	public enum OnUnhandledException
	{
		Crash,
		SilentExit
	}

	public enum StrippingLevel
	{
		Disabled,
		StripAssemblies,
		StripByteCode
	}

	public enum WritePermission
	{
		Internal,
		External
	}

	public enum InstallationLocation
	{
		Automatic,
		External,
		Internal
	}

	public enum DeviceFilter
	{
		Fat,
		ArMv7,
		X86
	}
}