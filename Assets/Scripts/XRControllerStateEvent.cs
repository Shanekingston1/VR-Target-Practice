using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRControllerStateEvents : MonoBehaviour
{
    public GameObject leftControllerModel;
    public GameObject rightControllerModel;

    private XRGrabInteractable grabbable;

    public void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(OnSelectEntered);
        StartCoroutine(ToggleControllerVisualCoroutine());
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        ToggleControllerVisual();
    }

    public IEnumerator ToggleControllerVisualCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        ToggleControllerVisual();
    }

    public void ToggleControllerVisual()
    {
        leftControllerModel.SetActive(false);
        rightControllerModel.SetActive(false);
    }
}