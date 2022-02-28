namespace Appalachia.Utility.Events.Contracts
{
    public interface IChangePublisher
    {
        void OnChanged();
        void SubscribeToChanges(AppaEvent.Handler handler);
        void UnsubscribeFromChanges(AppaEvent.Handler handler);
    }
}
