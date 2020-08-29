using System.Reflection;
using UnityEngine;

namespace CustomEventSystem.Core
{
    public readonly struct TargetData
    {
        private readonly MethodInfo info;
        private readonly MonoBehaviour target;

        public TargetData(MethodInfo _info, MonoBehaviour _behaviour)
        {
            info = _info;
            target = _behaviour;
        }

        public void InvokeTargetEvent()
        {
            info.Invoke(target, null);
        }

        public void InvokeTargetEvent(object[] _args)
        {
            info.Invoke(target, _args);
        }
    }
}