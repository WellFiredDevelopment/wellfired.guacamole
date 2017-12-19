namespace WellFired.Guacamole.DataBinding
{
	public enum BindingMode
	{
		/// <summary>
		/// If I bind a property and change that property on the backing store, it will be reflected on the bound object
		/// </summary>
		OneWay,

		/// <summary>
		/// If I change either the property or the backing store, the other will be reflected
		/// </summary>
		TwoWay,

		/// <summary>
		/// If I bind a property to the backing store, the only way to modify this property is to modify the one on 
		/// the backing store 
		/// </summary>
		ReadOnly
	}
}