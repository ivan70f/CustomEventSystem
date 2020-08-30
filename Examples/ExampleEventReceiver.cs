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

        /// <summary>
        /// Argumets should be in the same order as you passed it throw InvokeEvent function
        /// </summary>
        /// <param name="_stringArgument"></param>
        /// <param name="_intArgument"></param>
        private void OnEventWithArgumentsExample(string _stringArgument, int _intArgument)
        {
            Debug.Log(_stringArgument + "  " + _intArgument);
        }
    }
}