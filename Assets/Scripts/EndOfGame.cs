using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndOfGame : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private UnityEvent unityEvent = null;
    [SerializeField] private int timeTillEnd = 5;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CloseApplication());
    }

    private IEnumerator CloseApplication()
    {
        unityEvent.Invoke();
        yield return new WaitForSeconds(timeTillEnd);
        Application.Quit();
    }
}
