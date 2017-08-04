using System;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public struct FileSize
    {
        private const float EquivalenceTolerance = 100f; 
        
        public bool Equals(FileSize other)
        {
            return SizeInKb.Equals(other.SizeInKb) && SizeInMb.Equals(other.SizeInMb);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FileSize && Equals((FileSize) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SizeInKb.GetHashCode() * 397) ^ SizeInMb.GetHashCode();
            }
        }

        public static bool operator ==(FileSize a, FileSize b)
        {
            return Math.Abs(a.SizeInKb - b.SizeInKb) < EquivalenceTolerance;
        }

        public static bool operator !=(FileSize a, FileSize b)
        {
            return !(a == b);
        }
        
        public static bool operator >(FileSize a, FileSize b)
        {
            return a.SizeInKb - b.SizeInKb > EquivalenceTolerance;
        }

        public static bool operator <(FileSize a, FileSize b)
        {
            return b.SizeInKb - a.SizeInKb > EquivalenceTolerance;
        }

        public static FileSize operator +(FileSize a, FileSize b)
        {
            return new FileSize(a.SizeInKb + b.SizeInKb);
        }

        public readonly float SizeInKb;
        public readonly float SizeInMb;

        public FileSize(float sizeInKb)
        {
            SizeInKb = sizeInKb;
            SizeInMb = sizeInKb / 1024;
        }

        public override string ToString()
        {
            return $"{SizeInKb} kb";
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