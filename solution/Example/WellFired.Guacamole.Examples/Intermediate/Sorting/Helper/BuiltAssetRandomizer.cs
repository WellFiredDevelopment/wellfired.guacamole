using System;
using WellFired.Guacamole.Examples.Intermediate.Sorting.Model;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting.Helper
{
    public static class BuiltAssetRandomizer
    {
        private static readonly string[] PathParts1 = { "Assets/", "Assets/Folder/", "Resources/", "Streaming Assets/", "Custom/", "Editor/" };
        private static readonly string[] PathParts2 = { "Folder1/", "Character/", "Test/", "Art/", "Another/Test/", "SomeFolder/" };
        private static readonly string[] PathParts3 = { "Code/", "Scripts/", "Things/", "Atlases/", "Textures/", "Audio Clips/" };

        public static BuildAssetData Create(Random random)
        {
            return new BuildAssetData
            {
                Path = GetPath(random),
                BeforeSize = random.Next(0, 999999),
                AfterSize = random.Next(0, 999999)
            };
        }
        
        private static string GetPath(Random random)
        {
            var path = string.Empty;
            var partCount = random.Next(0, 3);
            while (partCount >= 0)
            {
                var index = 2 - partCount;
                switch (index)
                {
                    case 2:
                        path += PathParts1[random.Next(0, PathParts1.Length)];
                        break;
                    case 1:
                        path += PathParts2[random.Next(0, PathParts1.Length)];
                        break;
                    case 0:
                        path += PathParts3[random.Next(0, PathParts1.Length)];
                        break;
                }
                partCount--;
            }
            return path;
        }
    }
}