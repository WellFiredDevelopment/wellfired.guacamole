using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Converter.Default
{
	public class BindableObjectWithNullableProperty : BindableObject
	{
		[PublicAPI] public static readonly BindableProperty CanBeNullProperty = BindableProperty.Create<BindableObjectWithNullableProperty, object>(
			default(object),
			BindingMode.TwoWay,
			v => v.CanBeNull
		);
		
		[PublicAPI]
		public object CanBeNull
		{
			get => GetValue(CanBeNullProperty);
			set => SetValue(CanBeNullProperty, value);
		}
	}
}