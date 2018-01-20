using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist
{
    public class TaskistWindow : Window
    {
        public TaskistWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) : base(logger, persistantData, platformProvider)
        {
            StyleDictionary = new StyleDictionary(
                logger,
                new Dictionary<Type, Style> {
                    { typeof(FilterCell), Styles.FilterCell.Style },
                    { typeof(TaskCell), Styles.TaskCell.Style }
                }
            );
            
            SetContent(new TaskistMainPage());
        }
    }
}