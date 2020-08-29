using CustomEventSystem.Core;
using UnityEngine;

namespace CustomEventSystem.Examples
{
    public class ExampleEventInvoker : MonoBehaviour
    {
        private EventsHandler handler;
        
        private void Awake()
        {
            handler = GetComponent<EventsHandler>();
        }

        private void Start()
        {
            handler.InvokeEvent(EventName.OnEventExample);
            
            // Elements in array should be in the same order
            // as they placed in receive method
            object[] _arguments = new object[]
            {
                "Test string argument",
                123
            };
            
            handler.InvokeEvent(EventName.OnEventWithArgumentsExample, _arguments);
        }
    }
}