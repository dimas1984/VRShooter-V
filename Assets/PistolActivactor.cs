using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PistolActivator : MonoBehaviour
{
    public InputActionProperty toggleAction;
    public bool activateOnStart = false;

    public List<GameObject> goToActivateOnPistolActivated;
    public List<GameObject> goToActivateOnPistolDeactivated;

    private bool isActivated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActivated = activateOnStart;
        ApplyActivationState();
    }

    private void Update()
    {
        if (toggleAction.action.triggered)
        {
            isActivated = !isActivated;
            ApplyActivationState();
        }
    }

    public void ApplyActivationState()
    {
        foreach (var item in goToActivateOnPistolActivated)
        {
            item.SetActive(isActivated);
        }

        foreach (var item in goToActivateOnPistolDeactivated)
        {
            item.SetActive(!isActivated);
        }
    }
}
