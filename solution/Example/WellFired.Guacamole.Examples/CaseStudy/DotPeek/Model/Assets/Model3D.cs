namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets
{
    public class Model3D : Asset
    {
        public override string Path { get; set; }
        public override FileSize ImportedSize { get; set; }
        public override FileSize RawSize { get; set; }
        public override float Percentage { get; set; }
    }
}