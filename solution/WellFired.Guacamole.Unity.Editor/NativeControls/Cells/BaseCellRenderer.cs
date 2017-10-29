using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Cells
{
	public abstract class BaseCellRenderer : BaseRenderer
	{
		protected virtual bool CanMouseOver { get; } = true;
		
		public override void Render(UIRect renderRect)
		{
			var cell = Control as Cell;
			Debug.Assert(cell != null, "cell != null");

			if(CanMouseOver)
				EditorGUIUtility.AddCursorRect(UnityRect, MouseCursor.Link);

			if (Control.ControlState != ControlState.Disabled)
			{
				if (UnityEngine.Event.current.isMouse &&
				    UnityEngine.Event.current.rawType == EventType.mouseUp &&
				    renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition))
				{
					cell.Container.SelectedItem = cell.BindingContext;
				}
			}

			base.Render(renderRect);

			GUI.SetNextControlName(Control.Id);
		}
	}
}