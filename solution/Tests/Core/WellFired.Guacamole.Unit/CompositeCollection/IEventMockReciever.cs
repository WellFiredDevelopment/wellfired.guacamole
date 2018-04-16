using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
	public interface IEventMockReciever
	{
		void Receive(object sender, NotifyCollectionChangedEventArgs e);
	}
}