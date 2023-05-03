namespace Actors.Upgrades
{
    public interface IActorStatsReceiver
    {
        public void ApplyDynamicStats(ActorStatsSo actorStatsSo);
    }
}