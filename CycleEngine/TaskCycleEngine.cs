namespace CycleEngine
{
    public sealed class TaskCycleEngine:BaseTaskCycleEngine
    {
        private ICycleAction _cycleAction;
        public TaskCycleEngine(ICycleAction cycleAction)
        {
            this._cycleAction = cycleAction;
        }

        protected override bool DoDetect()
        {
            return this._cycleAction.EngineAction();
        }
    }
}
