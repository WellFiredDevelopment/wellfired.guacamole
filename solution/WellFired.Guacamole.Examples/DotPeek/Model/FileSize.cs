using System;

namespace WellFired.Guacamole.Examples.DotPeek.Model
{
    public struct FileSize
    {
        private const float EquivalenceTolerance = 512f; 
        
        public readonly float SizeInKB;
        public readonly float SizeInMB;

        public FileSize(float sizeInKB)
        {
            SizeInKB = sizeInKB;
            SizeInMB = sizeInKB / 1024;
        }

        public static bool operator ==(FileSize a, FileSize b)
        {
            return Math.Abs(a.SizeInKB - b.SizeInKB) < EquivalenceTolerance;
        }

        public static bool operator !=(FileSize a, FileSize b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{SizeInKB} kb";
        }

        public static FileSize ConvertToFileSize(string size, string unit)
        {
            const string b = "b";
            const string kb = "kb";
            const string mb = "mb";
            const string gb = "gb";

            var sizeFloat = float.Parse(size);
            float sizeInKb = -1;
            switch (unit)
            {
                case b:
                    sizeInKb = sizeFloat / 1024f;
                    break;

                case kb:
                    sizeInKb = sizeFloat;
                    break;

                case mb:
                    sizeInKb = sizeFloat * 1024f;
                    break;

                case gb:
                    sizeInKb = sizeFloat * 1024f * 1024f;
                    break;
            }

            return new FileSize(sizeInKb);
        }
    }
}