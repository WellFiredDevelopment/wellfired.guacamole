namespace WellFired.Guacamole.Views
{
    public interface IListensToVdsChanges
    {
        void ItemLeftVds(int vdsIndex, bool front);
        void ItemEnteredVds(int vdsIndex, bool front);
    }
}