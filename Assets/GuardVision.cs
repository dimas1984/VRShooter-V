using UnityEngine;

public class GuardVision : MonoBehaviour
{
    public Transform visionOrigin;
    public float viewDistance;
    public float viewAngle;
    public Transform target;

    public Color defaultColor;
    public Color canSeeColor;
    public Color debugColor;

    public LayerMask visionMask;
    public float sphereCastRadius = 0.2f;

    private bool canSeeTarget = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
    //    bool currentCanSeeTarget = CanSeeTarget();
    //    if (!canSeeTarget && currentCanSeeTarget)
    //    {
    //        GameManager.singleton.GameOver();
    //    }

    //    canSeeTarget = currentCanSeeTarget;
    //}

    public bool CanSeeTarget()
    {
        Vector3 toTarget = target.position - visionOrigin.position;
        float distance = toTarget.magnitude;

        if (distance > viewDistance)
        {
            return false;
        }

        float angle = Vector3.Angle(visionOrigin.forward, toTarget);
        if (angle > viewAngle / 2)
        {
            return false;
        }

        if (Physics.SphereCast(visionOrigin.position, sphereCastRadius, toTarget.normalized, out RaycastHit hit, toTarget.magnitude, visionMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform != target)
                return false;
        }

        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.color = debugColor;
        //UnityEditor.Handles.color = canSeeTarget ? canSeeColor : defaultColor;
        UnityEditor.Handles.DrawSolidArc(visionOrigin.position, visionOrigin.up, visionOrigin.forward, viewAngle / 2, viewDistance);
        UnityEditor.Handles.DrawSolidArc(visionOrigin.position, -visionOrigin.up, visionOrigin.forward, viewAngle / 2, viewDistance);
    }
#endif
}
