﻿﻿using System.ComponentModel;
 using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
 using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(TextEntryView), typeof(TextEntryViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{	
	public class TextEntryViewRenderer : BaseRenderer
	{
		private string _textToDisplay;
		private bool _wasFocused;
		
		public override UISize? NativeSize
		{
			get
			{
				var entry = Control as TextEntryView;
				// ReSharper disable once PossibleNullReferenceException
				return Style.CalcSize(new GUIContent(entry.Text)).ToUISize();
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();

			var entry = (TextEntryView)Control;
			if(ShouldShowPlaceholder())
				SetupWithPlaceholderStyle(entry, Style);
			else
				SetupWithNonePlaceholderStyle(entry, Style);
		}

		public override void Create()
		{
			base.Create();
			
			var entry = (TextEntryView)Control;
			_textToDisplay = ShouldShowPlaceholder() ? entry.PlaceholderText : entry.Text;
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var entry = (TextEntryView)Control;
			
			var textResult = EditorGUI.TextField(UnityRect, _textToDisplay, Style);

			if (ShouldShowPlaceholder())
				SetupWithPlaceholderStyle(entry, Style);
			else
				SetupWithNonePlaceholderStyle(entry, Style);

			var isFocused = false;
			var activeTextEditor = (TextEditor)EditorGUIExtensions.ActiveEditor;
			if (activeTextEditor != null)
				isFocused = EditorGUIUtilityExtensions.LastControlId == activeTextEditor.controlID;

			if (ShouldShowPlaceholder())
			{
				_textToDisplay = entry.PlaceholderText;
				
				// The user has typed some extra characters here, due to the way this control works, those
				// characters have to be at the start of the string, therefore we can simply extract that
				// data and that is our new string to display.
				if (textResult.Length > entry.PlaceholderText.Length)
				{
					var delta = textResult.Length - entry.PlaceholderText.Length;
					entry.Text = textResult.Substring(0, delta);
					_textToDisplay = entry.Text;

					if (isFocused)
						activeTextEditor.text = _textToDisplay;
				}
			}
			else
			{
				entry.Text = textResult;
				_textToDisplay = textResult;
			}
			
			// Lost Focus
			if (!isFocused && _wasFocused)
			{
				entry.OnFocusLost?.Execute();
				_textToDisplay = ShouldShowPlaceholder() ? entry.PlaceholderText : entry.Text;
			}

			// The user has pressed return
			if (isFocused && UnityEngine.Event.current.isKey && UnityEngine.Event.current.keyCode == KeyCode.Return)
				entry.OnInputEnter?.Execute();
			
			// If we should show placeholder, we want to ensure this control renders the placeholder text and that
			// the caret stays at index 0.
			if (ShouldShowPlaceholder())
			{
				if (isFocused)
				{
					activeTextEditor.SelectNone();
					activeTextEditor.MoveTextStart();
					activeTextEditor.text = _textToDisplay;
				}
			}

			_wasFocused = isFocused;
		}

		public override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnViewPropertyChanged(sender, e);

			var entry = (TextEntryView)Control;

			if (e.PropertyName == TextEntryView.TextProperty.PropertyName)
			{
				if (ShouldShowPlaceholder())
					SetupWithPlaceholderStyle(entry, Style);
				else
				{
					SetupWithNonePlaceholderStyle(entry, Style);
					
					// If someone has programmatically editted the text (due to a binding), we want to
					// tell the renderer we want to display that text, we must also force Unity's editor
					// to display this text, otherwise it forever shows the cached value.
					_textToDisplay = entry.Text;
					if (EditorGUIExtensions.ActiveEditor is TextEditor textEditor)
						textEditor.text = _textToDisplay;
				}
			}

			if (e.PropertyName == TextEntryView.PlaceholderTextProperty.PropertyName)
				_textToDisplay = ShouldShowPlaceholder() ? entry.PlaceholderText : entry.Text;
			
			if (e.PropertyName == TextEntryView.HorizontalTextAlignProperty.PropertyName || e.PropertyName == TextEntryView.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			if (e.PropertyName == TextEntryView.TextColorProperty.PropertyName)
			{
				Style.focused.textColor = entry.TextColor.ToUnityColor();
				Style.active.textColor = entry.TextColor.ToUnityColor();
				Style.hover.textColor = entry.TextColor.ToUnityColor();
				Style.normal.textColor = entry.TextColor.ToUnityColor();
			}
		}

		private static void SetupWithNonePlaceholderStyle(TextEntryView entry, GUIStyle style)
		{
			style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);
			style.focused.textColor = entry.TextColor.ToUnityColor();
			style.active.textColor = entry.TextColor.ToUnityColor();
			style.hover.textColor = entry.TextColor.ToUnityColor();
			style.normal.textColor = entry.TextColor.ToUnityColor();
		}

		private static void SetupWithPlaceholderStyle(TextEntryView entry, GUIStyle style)
		{
			style.alignment = UITextAlignExtensions.Combine(entry.PlaceholderHorizontalTextAlign, entry.PlaceholderVerticalTextAlign);
			style.focused.textColor = entry.PlaceholderTextColor.ToUnityColor();
			style.active.textColor = entry.PlaceholderTextColor.ToUnityColor();
			style.hover.textColor = entry.PlaceholderTextColor.ToUnityColor();
			style.normal.textColor = entry.PlaceholderTextColor.ToUnityColor();
		}

		private bool ShouldShowPlaceholder()
		{
			var entry = (TextEntryView)Control;
			return string.IsNullOrEmpty(entry.Text);
		}
	}
}