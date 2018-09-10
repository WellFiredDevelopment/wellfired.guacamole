using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Cells;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(Cell), typeof(BaseCellRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
	public class BaseCellRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var cell = (Cell) Control;

			if (cell.CanMouseOver)
				EditorGUIUtility.AddCursorRect(UnityRect, MouseCursor.Link);

			if (Control.ControlState != ControlState.Disabled && cell.CanMouseOver)
			{	
				if (UnityEngine.Event.current.LeftMouseUp() &&
				    renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition))
				{
					if (!cell.Container.CanMultiSelect ||
					    !UnityEngine.Event.current.control &&
					    !UnityEngine.Event.current.command)
					{
						MainThreadRunner.ExecuteBeforeLayout(() =>
						{
							//we first set to null otherwise if we select the same cell, the binding will not execute
							//the logic that unselect other cells.
							cell.Container.SelectedItem = null;
							cell.Container.SelectedItem = cell.BindingContext;
						});
					}
					else if(!cell.IsSelected)
					{
						MainThreadRunner.ExecuteBeforeLayout(() => cell.Container.SelectedItems.Add(cell.BindingContext));
					}
				}
			}
			
			base.Render(renderRect);

			//todo : check with Terry if it is necessary here
			GUI.SetNextControlName(Control.Id);
		}
	}
}