using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRControllerStateEvent : MonoBehaviour
{
   

    // Reference to the controller's visual
    public GameObject controllerVisual;

    private void Start()
    {
        // Check if the controller and controller visual are assigned
        if ( !controllerVisual)
        {
            Debug.LogError("Controller or controller visual not assigned.");
            return;
        }

        // Find all interactables in the scene
        XRBaseInteractable[] interactables = FindObjectsOfType<XRBaseInteractable>();

        // Add listeners to each interactable's select entered and exited events
        foreach (XRBaseInteractable interactable in interactables)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
            interactable.selectExited.AddListener(OnSelectExited);
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args) 
    {
        // Toggle the controller's visual off
        ToggleVisual(false);
    }

    public void OnSelectExited(SelectExitEventArgs args)  
    {
        // Toggle the controller's visual on
        ToggleVisual(true);
    }

    public void ToggleVisual(bool enable)
    {
        // Toggle the controller's visual
        controllerVisual.SetActive(enable);
    }
}