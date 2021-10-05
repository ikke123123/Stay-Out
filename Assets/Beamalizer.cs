using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beamalizer : MonoBehaviour
{
    [SerializeField] private float offIntensity = 2;
    [SerializeField] private float onIntensity = 15;
    [SerializeField] private Color offColor = Color.red;
    [SerializeField] private Color onColor = Color.white;
    [SerializeField] private float waitTime = 5;
    [SerializeField] private float fireTime = 2;
    [SerializeField] private float startOffset = 0;
    [SerializeField] private Vector3 rayDirection = new Vector3();
    [SerializeField] private float raycastStep = 0.11f;

    [Header("Debug")]
    [SerializeField] private bool active = true;

    private Light beam;

    private void Start()
    {
        beam = GetComponent<Light>();
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        yield return new WaitForSeconds(startOffset);
        while (active)
        {
            beam.intensity = offIntensity;
            beam.color = offColor;
            yield return new WaitForSeconds(waitTime);
            beam.intensity = onIntensity;
            beam.color = onColor;
            RaycastHit raycastHit;
            Ray castingRay;
            for (int t = 0; t < 4; t++)
            {
                for (int i = -3; i < 4; i++)
                {
                    for (int j = -3; j < 4; j++)
                    {
                        if ((i < 0 ? i * -1 : i) + (j < 0 ? j * -1 : j) < 4)
                        {
                            castingRay = new Ray(transform.position, rayDirection + new Vector3(raycastStep * i, 0, raycastStep * j));
                            Debug.DrawRay(castingRay.origin, castingRay.direction, Color.red);
                            if (Physics.SphereCast(castingRay.origin, 0.3f, castingRay.direction, out raycastHit, 20))
                            {
                                if (raycastHit.collider.CompareTag("Player"))
                                {
                                    raycastHit.collider.gameObject.GetComponent<KillPlayer>().Kill();
                                }
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(fireTime / 4);
            }
        }
    } 
}
