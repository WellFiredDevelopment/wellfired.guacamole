using System.ComponentModel;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(NumberEntry), typeof(NumberEntryRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class NumberEntryRenderer : BaseRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var numberEntry = Control as NumberEntry;
				// ReSharper disable once PossibleNullReferenceException
				return Style.CalcSize(new GUIContent(numberEntry.Number.ToString(CultureInfo.InvariantCulture))).ToUISize();
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();

			var entry = (NumberEntry) Control;
			Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);
			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var entry = (NumberEntry)Control;
			var newNumber = EditorGUI.FloatField(UnityRect, entry.Number, Style);
			if (Equals(newNumber, entry.Number))
				entry.Number = newNumber;
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			var entry = (NumberEntry)Control;

			if (e.PropertyName == NumberEntry.HorizontalTextAlignProperty.PropertyName || e.PropertyName == NumberEntry.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			if (e.PropertyName != NumberEntry.TextColorProperty.PropertyName) 
				return;
			
			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();
		}
	}
}