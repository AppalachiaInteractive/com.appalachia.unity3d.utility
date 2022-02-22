namespace Appalachia.Utility.Events.Contracts
{
    public interface IChangePublisher
    {
        public void SubscribeToChanges(AppaEvent.Handler handler);
        public void UnsubscribeFromChanges(AppaEvent.Handler handler);
    }
}
