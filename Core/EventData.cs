using System.Linq;

namespace CustomEventSystem.Core
{
    public readonly struct EventData
    {
        /// <summary>
        /// True if Object hierarchy have 1 or more declaration of event
        /// </summary>
        public readonly bool haveTargets;

        private readonly TargetData[] targets;

        public EventData(TargetData[] _targets)
        {
            haveTargets = _targets.Any();
            
            targets = _targets;
        }

        /// <summary>
        /// Invoke event for all cached targets.
        /// </summary>
        public void InvokeEvent()
        {
            for (int i = 0; i < targets.Length; i++)
                targets[i].InvokeTargetEvent();
        }

        /// <summary>
        /// Invoke event with arguments for all cached targets.
        /// </summary>
        /// <param name="_args"> Event arguments </param>
        public void InvokeEvent(object[] _args)
        {
            for (int i = 0; i < targets.Length; i++)
                targets[i].InvokeTargetEvent(_args);
        }
    }
}