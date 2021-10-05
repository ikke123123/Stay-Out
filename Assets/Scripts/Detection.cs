using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private BadGuyMove rayDirection = null;
    [SerializeField] private bool horizontalMove = false;

    private void Update()
    {
        RaycastHit raycastHit;
        Ray castingRay;
        for (int i = -1; i < 2; i++)
        {
            castingRay = new Ray(transform.position, rayDirection.rayDirection + new Vector3(horizontalMove ? 0.26f * i : 0, 0, horizontalMove ? 0 : 0.26f * i));

            Debug.DrawRay(castingRay.origin, castingRay.direction, Color.white);
            if (Physics.Raycast(castingRay, out raycastHit, 20))
            {
                if (raycastHit.collider.CompareTag("Player"))
                {
                    raycastHit.collider.gameObject.GetComponent<KillPlayer>().Kill();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<KillPlayer>().Kill();
        }
    }
}
