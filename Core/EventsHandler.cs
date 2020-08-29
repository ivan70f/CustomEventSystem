using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomEventSystem.Core
{
    public class EventsHandler : MonoBehaviour
    {
        private readonly Dictionary<EventName, EventData> events
            = new Dictionary<EventName, EventData>();

        private void Awake()
        {
            MonoBehaviour[] _behaviours = GetComponentsInChildren<MonoBehaviour>();

            for (int i = 0; i < Enum.GetValues(typeof(EventName)).Length; i++)
                AssignTargetsToEventType((EventName) i, _behaviours);
        }

        public void InvokeEvent(EventName _name)
        {
            EventData _data = GetEventData(_name);

            if (_data.haveTargets)
                _data.InvokeEvent();
        }

        public void InvokeEvent(EventName _name, object[] _args)
        {
            EventData _data = GetEventData(_name);

            if (_data.haveTargets)
                _data.InvokeEvent(_args);
        }

        private EventData GetEventData(EventName _name)
        {
            if (events.ContainsKey(_name) == false)
                AssignTargetsToEventType(
                    _name, GetComponentsInChildren<MonoBehaviour>());

            return events[_name];
        }

        private void AssignTargetsToEventType(EventName _name, MonoBehaviour[] _behaviours)
        {
            events.Add(_name, new EventData(CacheTargets(_name, _behaviours)));
        }

        private TargetData[] CacheTargets(EventName _name, MonoBehaviour[] _behaviours)
        {
            List<TargetData> _datas = new List<TargetData>();

            for (int i = 0; i < _behaviours.Length; i++)
            {
                if (GetMethodInfo(_name, _behaviours[i], out MethodInfo _info))
                    _datas.Add(new TargetData(_info, _behaviours[i]));
            }

            return _datas.ToArray();
        }

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