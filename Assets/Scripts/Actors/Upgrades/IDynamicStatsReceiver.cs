namespace Actors.Upgrades
{
    public interface IDynamicStatsReceiver
    {
        public void ApplyDynamicStats(ActorStatsSo actorStatsSo);
    }
}