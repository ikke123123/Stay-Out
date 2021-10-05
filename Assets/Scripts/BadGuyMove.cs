using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyMove : MonoBehaviour
{
    enum Position { p1, p2 };

    [Header("Timings")]
    [SerializeField] private float walkTime = 10;
    [SerializeField] private float stopTime = 5;

    [Header("Positions")]
    [SerializeField] private Vector2 p1 = new Vector2(0, 0);
    [SerializeField] private Vector2 p2 = new Vector2(0, 0);

    [HideInInspector] public bool disableWalking = false;
    [HideInInspector] public Vector3 rayDirection = new Vector3();

    private Position prevPosition;
    private Position targetPosition;

    private void Start()
    {
        StartCoroutine(MoveCycle());
    }

    private IEnumerator MoveCycle()
    {
        prevPosition = Position.p1;
        transform.position = new Vector3(p1.x, transform.position.y, p1.y);
        targetPosition = Position.p2;
        while (disableWalking == false)
        {
            Vector2 prev = GetVector2(prevPosition);
            Vector2 target = GetVector2(targetPosition);
            rayDirection = prev.x > target.x ? Vector3.left : (prev.y > target.y ? Vector3.back : (prev.y < target.y ? Vector3.forward : Vector3.right));
            for (float i = 0; walkTime > i; i += Time.deltaTime)
            {
                transform.position = new Vector3(Mathf.Lerp(prev.x, target.x, CodeLibrary.Remap(i, 0, walkTime, 0, 1)), transform.position.y, Mathf.Lerp(prev.y, target.y, CodeLibrary.Remap(i, 0, walkTime, 0, 1)));
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(stopTime);
            Transition();
            transform.Rotate(new Vector3(0, 180));
        }
    }

    private Vector2 GetVector2(Position position)
    {
        return position == Position.p1 ? p1 : p2;
    }

    private void Transition()
    {
        targetPosition = prevPosition;
        prevPosition = prevPosition == Position.p1 ? Position.p2 : Position.p1;
    }
}
