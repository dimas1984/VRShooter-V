using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class PatrolPoint
{
    public Transform point;
    public float waitSeconds;
}


public class SimplePatrol : MonoBehaviour
{
    public List<PatrolPoint> points;

    public Animator animator;

    public float moveSpeed = 1.5f;
    public float rotationSpeed = 5;
    public float arriveDisttance = 0.05f;

    private int currentIndex = 0;
    private bool waiting = false;


    // Update is called once per frame
    void Update()
    {
        if (waiting)
            return;

        PatrolPoint currentPoint = points[currentIndex];
        Vector3 targetPos = currentPoint.point.position;
        Vector3 targetDirection = targetPos - transform.position;
        targetDirection.y = 0;

        transform.position = transform.position + targetDirection.normalized * moveSpeed * Time.deltaTime;
        Quaternion targetRot = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
         
        float dist = targetDirection.magnitude;
        if (dist < arriveDisttance)
        {
            StartCoroutine(PatrolWait(currentPoint.waitSeconds));
        }

        animator.SetBool("IsWalking", !waiting);

        if (waiting)
        {
            animator.speed = 1;
        }
        else
        {
            animator.speed = moveSpeed;
        }
    }

    IEnumerator PatrolWait(float seconds)
    {
        waiting = true;
        yield return new WaitForSeconds(seconds);
        waiting = false;
        currentIndex = (currentIndex + 1) % points.Count;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Count; i++)
        {
            PatrolPoint p = points[i];
            PatrolPoint next = points[(i + 1) % points.Count];

            Gizmos.DrawLine(p.point.position, next.point.position);
        }
    }
}
