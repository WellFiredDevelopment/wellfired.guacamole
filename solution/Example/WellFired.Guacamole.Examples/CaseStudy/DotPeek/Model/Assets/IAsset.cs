namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets
{
	public interface IAsset
	{
		string Path { get; set; }
		FileSize ImportedSize { get; set; }
		FileSize RawSize { get; set; }
		float Percentage { get; set; }
	}
}