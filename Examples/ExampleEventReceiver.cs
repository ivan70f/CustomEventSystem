using UnityEngine;

namespace CustomEventSystem.Examples
{
    public class ExampleEventReceiver : MonoBehaviour
    {
        /// <summary>
        /// Methods can be private or public, static or non-static
        /// </summary>
        private void OnEventExample()
        {
            Debug.Log("Example event invoke");
        }

        private void OnEventWithArgumentsExample(string _stringArgument, int _intArgument)
        {
            Debug.Log(_stringArgument + "  " + _intArgument);
        }
    }
}