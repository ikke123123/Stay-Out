using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFence : MonoBehaviour
{
    enum Position { open, closed };

    [Header("Setup")]
    [SerializeField] private Transform fence = null;
    [SerializeField] private float openPosition = -2.05f;
    [SerializeField] private float closePosition = 0f;
    [Tooltip("Time in seconds.")] [SerializeField, Range(0.5f, 5f)] private float openingSpeed = 1;

    [Header("Debug")]
    public bool open = false;

    private Position position = Position.closed;

    private void Start()
    {
        open = false;
        position = Position.closed;
    }

    private void Update()
    {
        if (open == true && position == Position.closed)
        {
            StartCoroutine(FenceMove(closePosition, openPosition));
            position = Position.open;
        }
        if (open == false && position == Position.open)
        {
            StartCoroutine(FenceMove(openPosition, closePosition));
            position = Position.closed;
        }
    }

    public void Open()
    {
        open = true;
    }

    public void Close()
    {
        open = false;
    }

    private IEnumerator FenceMove(float currentPos, float newPos)
    {
        for (float i = 0; openingSpeed > i; i += Time.deltaTime)
        {
            CodeLibrary.SetLocalX(fence, Mathf.Lerp(currentPos, newPos, CodeLibrary.Remap(i, 0, openingSpeed, 0, 1)));
            yield return new WaitForFixedUpdate();
        }
        CodeLibrary.SetLocalX(fence, newPos);
    }
}
