using UnityEngine;

public class GuardDeath : MonoBehaviour
{
    public Animator animator;
    public GuardVision guardVision;
    public SimplePatrol guardPatrol;
    public GameObject guardVisionDecal;

    public void TriggerDeath()
    {
        animator.SetTrigger("Dead");
        guardPatrol.enabled = false;
        guardVision.enabled = false;
        guardVisionDecal.SetActive(false);
    }
}
