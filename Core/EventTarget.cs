using System.Reflection;
using UnityEngine;

namespace CustomEventSystem.Core
{
    public readonly struct TargetData
    {
        /// <summary>
        /// Method Info is used to invoke event in one MonoBehaviour
        /// </summary>
        private readonly MethodInfo info;
        
        /// <summary>
        /// Target MonoBehaviour with declared event
        /// </summary>
        private readonly MonoBehaviour target;

        public TargetData(MethodInfo _info, MonoBehaviour _behaviour)
        {
            info = _info;
            target = _behaviour;
        }

        /// <summary>
        /// Invoke event on target
        /// </summary>
        public void InvokeTargetEvent()
        {
            info.Invoke(target, null);
        }

        /// <summary>
        /// Invoke event with arguments on target
        /// </summary>
        /// <param name="_args"> Event arguments </param>
        public void InvokeTargetEvent(object[] _args)
        {
            info.Invoke(target, _args);
        }
    }
}