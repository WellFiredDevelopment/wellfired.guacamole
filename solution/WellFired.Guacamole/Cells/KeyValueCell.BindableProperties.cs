using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Cells
{
	public partial class KeyValueCell
	{
		[PublicAPI] public static readonly BindableProperty KeyTextProperty = BindableProperty.Create<KeyValueCell, string>(
			string.Empty,
			BindingMode.TwoWay,
			cell => cell.KeyText
		);
		
		[PublicAPI] public static readonly BindableProperty ValueTextProperty = BindableProperty.Create<KeyValueCell, string>(
			string.Empty,
			BindingMode.TwoWay,
			cell => cell.ValueText
		);
		
		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty.Create<KeyValueCell, UIColor>(
			UIColor.Black,
			BindingMode.TwoWay,
			cell => cell.TextColor
		);
		
		[PublicAPI]
		public string KeyText
		{
			get => (string) GetValue(KeyTextProperty);
			set => SetValue(KeyTextProperty, value);
		}
		
		[PublicAPI]
		public string ValueText
		{
			get => (string) GetValue(ValueTextProperty);
			set => SetValue(ValueTextProperty, value);
		}
		
		[PublicAPI]
		public UIColor TextColor
		{
			get => (UIColor) GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}
	}
}