using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float idleSwitchTime = 1;
    [SerializeField] private float chargeTime = 2;
    [SerializeField] private float activeTime = 0.1f;
    [SerializeField] private Color activatedEmissionColor = Color.white;
    [SerializeField] private float activatedEmissionIntensity = 3;
    [SerializeField] private Light flashLight = null;
    [SerializeField] private MeshRenderer tripWire = null;

    [Header("Raycasting")]
    [SerializeField] private Vector3 rayDirection = new Vector3();

    [Header("Debug")]
    public bool idle = true;

    private MeshRenderer meshRender = null;
    private Color standardEmission = Color.red;
    private IEnumerator idleLoop = null;
    private Collider tripWireCollider = null;
    private float raycastStep = 0;

    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        standardEmission = meshRender.material.GetColor("_EmissionColor");
        tripWireCollider = GetComponent<Collider>();
        idleLoop = IdleLoop();
        StartCoroutine(idleLoop);
        raycastStep = 0.25f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(idleLoop);
            StartCoroutine(Activate());
            tripWireCollider.enabled = false;
        }
    }

    private IEnumerator IdleLoop()
    {
        while (idle)
        {
            meshRender.material.SetColor("_EmissionColor", standardEmission * 2);
            tripWire.material.SetColor("_EmissionColor", Color.white * -2);
            yield return new WaitForSeconds(idleSwitchTime);
            meshRender.material.SetColor("_EmissionColor", Color.white * -2);
            tripWire.material.SetColor("_EmissionColor", standardEmission * 2);
            yield return new WaitForSeconds(idleSwitchTime);
        }
    }

    private IEnumerator Activate()
    {
        tripWire.material.SetColor("_EmissionColor", Color.white * -2);
        for (float i = 0; i < chargeTime; i += Time.deltaTime)
        {
            meshRender.material.SetColor("_EmissionColor", activatedEmissionColor * Mathf.Lerp(1, activatedEmissionIntensity, CodeLibrary.Remap(i, 0, chargeTime, 0, 1)));
            yield return new WaitForFixedUpdate();
        }
        meshRender.material.SetColor("_EmissionColor", activatedEmissionColor * activatedEmissionIntensity);
        flashLight.enabled = true;
        RaycastHit raycastHit;
        Ray castingRay;
        for (float p = 0; p < activeTime; p += Time.deltaTime)
        {
            for (int i = -2; i < 3; i++)
            {
                castingRay = new Ray(transform.position, rayDirection + new Vector3(raycastStep * i, 0, 0));
                Debug.DrawRay(castingRay.origin, castingRay.direction, Color.red);
                if (Physics.Raycast(castingRay, out raycastHit, 20))
                {
                    if (raycastHit.collider.CompareTag("Player"))
                    {
                        raycastHit.collider.gameObject.GetComponent<KillPlayer>().Kill();
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(activeTime);
        flashLight.enabled = false;
        meshRender.material.SetColor("_EmissionColor", Color.white * -2);
    }
}
