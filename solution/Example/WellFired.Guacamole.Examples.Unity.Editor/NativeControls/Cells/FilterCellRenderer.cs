using UnityEditor;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells;
using WellFired.Guacamole.Examples.Unity.Editor.NativeControls.Cells;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;

[assembly: CustomRenderer(typeof(FilterCell), typeof(FilterCellRenderer))]
namespace WellFired.Guacamole.Examples.Unity.Editor.NativeControls.Cells
{
    public class FilterCellRenderer : BaseCellRenderer
    {
        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
            EditorGUI.LabelField(UnityRect, "", Style);
        }
    }
}