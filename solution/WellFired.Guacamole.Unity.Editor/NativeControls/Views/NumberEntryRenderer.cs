using System.ComponentModel;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(NumberEntryView), typeof(NumberEntryRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class NumberEntryRenderer : BaseRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var numberEntry = Control as NumberEntryView;
				// ReSharper disable once PossibleNullReferenceException
				return Style.CalcSize(new GUIContent(numberEntry.Number.ToString(CultureInfo.InvariantCulture))).ToUISize();
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();

			var entry = (NumberEntryView) Control;
			Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);
			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var entry = (NumberEntryView)Control;
			var newNumber = EditorGUI.FloatField(UnityRect, entry.Number, Style);
			if (Equals(newNumber, entry.Number))
				entry.Number = newNumber;
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			var entry = (NumberEntryView)Control;

			if (e.PropertyName == NumberEntryView.HorizontalTextAlignProperty.PropertyName || e.PropertyName == NumberEntryView.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			if (e.PropertyName != NumberEntryView.TextColorProperty.PropertyName) 
				return;
			
			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();
		}
	}
}