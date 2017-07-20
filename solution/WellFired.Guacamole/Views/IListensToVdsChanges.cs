namespace WellFired.Guacamole.Views
{
    public interface IListensToVdsChanges
    {
        void ItemLeftVds(int vdsIndex);
        void ItemEnteredVds(int vdsIndex, bool front);
    }
}