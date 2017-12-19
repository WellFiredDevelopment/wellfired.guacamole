using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
	public interface IEventMockReciever
	{
		void Recieve(object sender, NotifyCollectionChangedEventArgs e);
	}
}