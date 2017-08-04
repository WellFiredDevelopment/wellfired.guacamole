namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets
{
    public abstract class Asset : IAsset
    {
        public abstract string Path { get; set; }
        public abstract FileSize ImportedSize { get; set; }
        public abstract FileSize RawSize { get; set; }
        public abstract float Percentage { get; set; }

        public override string ToString()
        {
            return
                $"Type : {GetType()}, Path : {Path}, Imported size : {ImportedSize}, Raw Size : {RawSize}, " +
                $"Percentage : {Percentage}";
        }
    }
}