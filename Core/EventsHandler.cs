using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomEventSystem.Core
{
    /// <summary>
    /// This script should be attached to the top of Prefab hierarchy to cache all references
    /// </summary>
    public class EventsHandler : MonoBehaviour
    {
        [SerializeField] private bool cacheInactiveGameObjects = true;
        
        /// <summary>
        /// Dictionary of all events declared in scripts attached to GameObject or it's child
        /// </summary>
        private readonly Dictionary<EventName, EventData> events
            = new Dictionary<EventName, EventData>();

        /// <summary>
        /// Cache all events
        /// </summary>
        private void Awake()
        {
            MonoBehaviour[] _behaviours = GetAttachedComponents();

            for (int i = 0; i < Enum.GetValues(typeof(EventName)).Length; i++)
                AssignTargetsToEventType((EventName) i, _behaviours);
        }

        /// <summary>
        /// Invokes event on all cached targets. Note, that it will invoke event
        /// even if target GameObject is inactive.
        /// </summary>
        /// <param name="_name"> Event type defined in EventName enum </param>
        public void InvokeEvent(EventName _name)
        {
            EventData _data = GetEventData(_name);

            if (_data.haveTargets)
                _data.InvokeEvent();
        }
        
        /// <summary>
        /// Invokes event on all cached targets. Note, that it will invoke event
        /// even if target GameObject is inactive. Elements in object array should be placed
        /// in the same order as you placed it in target method.
        /// </summary>
        /// <param name="_name"> Event type defined in EventName enum </param>
        /// <param name="_args"> Event arguments </param>
        public void InvokeEvent(EventName _name, object[] _args)
        {
            EventData _data = GetEventData(_name);

            if (_data.haveTargets)
                _data.InvokeEvent(_args);
        }

        /// <summary>
        /// Get all MonoBehaviour scripts in the objects hierarchy.
        /// If cacheInactiveGameObjects is True it will include inactive GameObjects in result
        /// </summary>
        /// <returns> Returns array of Monobehaviours attached to Objects hierarchy</returns>
        private MonoBehaviour[] GetAttachedComponents()
        {
            return GetComponentsInChildren<MonoBehaviour>(cacheInactiveGameObjects);
        }

        /// <summary>
        /// Check if events dictionary actualy has EventData of selected type and
        /// add if if not.
        /// </summary>
        /// <param name="_name"> Event type</param>
        /// <returns> Event data of selected type</returns>
        private EventData GetEventData(EventName _name)
        {
            if (events.ContainsKey(_name) == false)
                AssignTargetsToEventType(
                    _name, GetAttachedComponents());

            return events[_name];
        }

        /// <summary>
        /// Add cached events to dictioanry
        /// </summary>
        /// <param name="_name"> Event type </param>
        /// <param name="_behaviours"> Target MonoBehaviours</param>
        private void AssignTargetsToEventType(EventName _name, MonoBehaviour[] _behaviours)
        {
            events.Add(_name, new EventData(GetTargets(_name, _behaviours)));
        }

        /// <summary>
        /// Find all targets of selected EventType in selected MonoBehaviours.
        /// </summary>
        /// <param name="_name"> Event type </param>
        /// <param name="_behaviours"> Selected MonoBehaviours </param>
        /// <returns> All behaviours from selected which contains event declaration </returns>
        private TargetData[] GetTargets(EventName _name, MonoBehaviour[] _behaviours)
        {
            List<TargetData> _datas = new List<TargetData>();

            for (int i = 0; i < _behaviours.Length; i++)
            {
                if (GetMethodInfo(_name, _behaviours[i], out MethodInfo _info))
                    _datas.Add(new TargetData(_info, _behaviours[i]));
            }

            return _datas.ToArray();
        }

        /// <summary>
        /// Check if MonoBehaviours contains declaration of concrete Event type.
        /// </summary>
        /// <param name="_name"> Event type </param>
        /// <param name="_behaviour"> Target behaviour </param>
        /// <param name="_info"> MethdodInfo </param>
        /// <returns> Will return true if MonoBehaviour contains declaration of event</returns>
        private bool GetMethodInfo(EventName _name, MonoBehaviour _behaviour, out MethodInfo _info)
        {
            string _methodName = Enum.GetName(typeof(EventName), _name);
            _info = null;

            if (string.IsNullOrEmpty(_methodName) == true)
                return false;

            _info = _behaviour.GetType().GetMethod(
                _methodName,
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.Static);

            if (_info == null)
                return false;
            
            return true;
        }
    }
}