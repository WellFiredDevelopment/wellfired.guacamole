using System;
using System.Collections.Generic;
using System.Text;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public class BuildOverview
    {
        public enum Category
        {
            Undefined,
            Textures,
            Meshes,
            Animations,
            Sounds,
            Shaders,
            OtherAssets,
            Levels,
            Scripts,
            IncludedDLLs,
            FileHeaders
        }

        public string CommitId = "Invalid";
        public string Platform = "Invalid";
        public string UnityVersion = "Invalid";
        public FileSize BuildSize = new FileSize();
        public DateTime BuildTime = DateTime.MinValue;
        public readonly List<BuildAssetSplit> BuildAssetSplits = new List<BuildAssetSplit>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Size Total {BuildSize}");
            foreach (var split in BuildAssetSplits)
                stringBuilder.Append($"\n{split}");

            return stringBuilder.ToString();
        }

        public class BuildAssetSplit
        {
            public readonly Category Category;

            public readonly float Percentage;

            public readonly FileSize Size;

            public BuildAssetSplit(Category category, FileSize size, float percentage)
            {
                Category = category;
                Size = size;
                Percentage = percentage;
            }

            public override string ToString()
            {
                return $"Category : {Category}, Size : {Size}, Percentage : {Percentage}";
            }

            private bool Equals(BuildAssetSplit other)
            {
                return Category == other.Category && Percentage.Equals(other.Percentage) && Size == other.Size;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                return obj.GetType() == GetType() && Equals((BuildAssetSplit) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (int) Category;
                    hashCode = (hashCode * 397) ^ Percentage.GetHashCode();
                    hashCode = (hashCode * 397) ^ Size.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}