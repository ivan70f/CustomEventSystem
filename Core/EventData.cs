using System.Linq;

namespace CustomEventSystem.Core
{
    public readonly struct EventData
    {
        public readonly bool haveTargets;

        private readonly TargetData[] targets;

        public EventData(TargetData[] _targets)
        {
            haveTargets = _targets.Any();
            
            targets = _targets;
        }

        public void InvokeEvent()
        {
            for (int i = 0; i < targets.Length; i++)
                targets[i].InvokeTargetEvent();
        }

        public void InvokeEvent(object[] _args)
        {
            for (int i = 0; i < targets.Length; i++)
                targets[i].InvokeTargetEvent(_args);
        }
    }
}