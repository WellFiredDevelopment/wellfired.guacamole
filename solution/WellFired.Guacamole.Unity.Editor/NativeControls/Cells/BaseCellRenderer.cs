using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
	public abstract class BaseCellRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var cell = (Cell) Control;

			if (cell.CanMouseOver)
				EditorGUIUtility.AddCursorRect(UnityRect, MouseCursor.Link);

			if (Control.ControlState != ControlState.Disabled && cell.CanMouseOver)
			{
				if (UnityEngine.Event.current.isMouse &&
				    UnityEngine.Event.current.rawType == EventType.MouseUp &&
				    renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition))
				{
					MainThreadRunner.ExecuteBeforeLayout(() => cell.Container.SelectedItem = cell.BindingContext);
				}
			}

			base.Render(renderRect);

			//todo : check with Terry if it is necessary here
			GUI.SetNextControlName(Control.Id);
		}
	}
}