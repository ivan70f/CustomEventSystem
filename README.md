# CustomEventSystem
This package allows you to use your custom events like Unity built-in event's (Awake, Start, OnEnable...).

Quick guide:
1. Add name of your event to enum file "EventName"
2. Attach to your gameObject script "EventsHandler"
3. In scripts on this gameObject or it's child gameObjects create method with the exactly the same name as you deifined in "EventName" file.
4. Invoke your event by using
```GetComponent<EventsHandler>().InvokeEvent(EventName.YOUREVENTNAME);```
  
If you want to use events with arguments you should create method with required argumetns and add new objects array with all your arguments in the same order as you defined in your method and pass throw InvokeEvent function as a second argument. Example:


```
// Create method
private void OnEventWithArgumentsExample(string _stringArgument, int _intArgument)
{
    Debug.Log(_stringArgument + "  " + _intArgument);
}
        
// Invoke event
object[] _arguments = new object[]
{
    "Test string argument",
    123
};
GetComponent<EventsHandler>().InvokeEvent(EventName.OnEventWithArgumentsExample, _arguments);
```
