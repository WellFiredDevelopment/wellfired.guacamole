using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist
{
    public class TaskistWindow : Window
    {
        public TaskistWindow(ILogger logger, INotifyPropertyChanged persistantData) : base(logger, persistantData)
        {
            StyleDictionary = new StyleDictionary(
                logger,
                new Dictionary<Type, Style> {
                    { typeof(FilterCell), Styles.FilterCell.Style }
                }
            );
            
            SetContent(new TaskistMainPage());
        }
    }
}