using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public static class ViewModelUtils
    {
        public static UIColor CompareSizeColor(FileSize newSize, FileSize previousSize)
        {
            UIColor color;
            if (newSize > previousSize)
            {
                color = UIColor.FromRGB(136, 0, 43);
            }
            else if (newSize < previousSize)
            {
                color = UIColor.FromRGB(0, 136, 43);
            }
            else
            {
                color = UIColor.FromRGB(40, 40, 40);
            }

            return color;
        }
    }
}