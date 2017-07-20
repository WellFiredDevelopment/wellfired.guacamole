﻿using System.Collections.Generic;
using System.Text;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public class BuildOverview
    {
        public struct BuildAssetSplit
        {
            public readonly Category Category;
            public readonly float Percentage;
            public FileSize Size;

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
        }

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

        public FileSize BuildSize;
        public readonly List<BuildAssetSplit> BuildAssetSplits = new List<BuildAssetSplit>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Size Total {BuildSize}");
            foreach (var split in BuildAssetSplits)
            {
                stringBuilder.Append($"\n{split}");
            }

            return stringBuilder.ToString();
        }
    }
}