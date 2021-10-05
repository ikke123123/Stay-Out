using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    /* 
        Instructions: Add the things you want to change to the UnityEvent.
        Add the keycode to activate the button to the KeyCode.
        DON'T FORGET TO MAKE A SPECIAL LAYER TO AVOID NEEDLESSLY WASTING CYCLES AND TO DENY FALSE TRIGGERS.
    */

    [Header("Setup")]
    [SerializeField] private UnityEvent unityEvent = null;
    [SerializeField] private KeyCode keyCode = KeyCode.None;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(keyCode))
        {
            unityEvent.Invoke();
        }
    }
}
