using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : MonoBehaviour
{
    private XRGrabInteractable grabInteractable; // XR Grab Interactable 组件

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(Grab);
            grabInteractable.selectExited.AddListener(Release);
        }
    }

    public bool IsGrabbed { get; private set; } = false;

    private void Grab(SelectEnterEventArgs arg)
    {
        IsGrabbed = true;
    }

    private void Release(SelectExitEventArgs arg)
    {
        IsGrabbed = false;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(Grab);
            grabInteractable.selectExited.RemoveListener(Release);
        }
    }
}
