namespace CycleEngine
{
    public sealed class TimerCycleEngine:BaseTimerCycleEngine
    {
        private ICycleAction _cycleAction;
        public TimerCycleEngine(ICycleAction cycleAction)
        {
            this._cycleAction = cycleAction;
        }
        protected override bool DoDetect()
        {
            return this._cycleAction.EngineAction();
        }
    }
}
