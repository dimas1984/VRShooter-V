using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Unity Input System

public class PistolShooter : MonoBehaviour
{
    public LayerMask layerMask;
    [Tooltip("Assign an InputAction (e.g. a button) from your Input Actions asset.")]
    public InputActionProperty shootingAction; // set this in the inspector

    public LineRenderer linePrefab;
    public GameObject rayImpactPrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5f;
    public float lineShowTimer = 0.3f;
    public AudioSource source;
    public AudioClip shootingAudioClip;

    private void Update()
    {
        if(shootingAction.action.triggered)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (source != null && shootingAudioClip != null)
        {
            source.PlayOneShot(shootingAudioClip);
        }

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint;

        //spawn impact
        if (hasHit)
        {
            endPoint = hit.point;
            GuardDeath guardDeath = hit.transform.GetComponent<GuardDeath>();
            if (guardDeath)
            {
                guardDeath.TriggerDeath();
            }
            else
            {
                Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
                if (rayImpactPrefab != null)
                {
                    GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
                    Destroy(rayImpact, .5f);
                }
            }
        }
        else
        {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
        }

        //spawn line
        if (linePrefab != null)
        {
            LineRenderer line = Instantiate(linePrefab);
            line.positionCount = 2;
            line.SetPosition(0, shootingPoint.position);
            line.SetPosition(1, endPoint);
            Destroy(line.gameObject, lineShowTimer);
        }
    }
}
