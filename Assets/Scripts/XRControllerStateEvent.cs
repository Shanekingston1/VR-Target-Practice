using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRControllerStateEvents : MonoBehaviour
{
    public GameObject leftControllerModel;
    public GameObject rightControllerModel;

    private XRController leftController;
    private XRController rightController;

    private void Update()
    {
        if (leftController != null)
        {
            // Access leftController properties or methods here
            Debug.Log("Left controller is not null");
        }
        else
        {
            Debug.LogError("Left controller is null!");
        }

        if (rightController != null)
        {
            // Access rightController properties or methods here
            Debug.Log("Right controller is not null");
        }
        else
        {
            Debug.LogError("Right controller is null!");
        }
    }

    private void Start()
    {

            Debug.Log("XRControllerStateEvents.Start() called");
            // Get the left and right controllers
            leftController = leftControllerModel.GetComponent<XRController>();
            rightController = rightControllerModel.GetComponent<XRController>();
            Debug.Log("Left controller: " + leftController);
            Debug.Log("Right controller: " + rightController);
      

        // Get the left and right controllers
        leftController = leftControllerModel.GetComponent<XRController>();
        rightController = rightControllerModel.GetComponent<XRController>();

        // Get the interactable components
        XRBaseInteractable leftInteractable = leftControllerModel.GetComponent<XRBaseInteractable>();
        XRBaseInteractable rightInteractable = rightControllerModel.GetComponent<XRBaseInteractable>();

        // Add event listeners
        leftInteractable.selectEntered.AddListener(OnSelectEnter);
        rightInteractable.selectEntered.AddListener(OnSelectEnter);
        leftInteractable.selectExited.AddListener(OnSelectExit);
        rightInteractable.selectExited.AddListener(OnSelectExit);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("LeftController"))
        {
            leftControllerModel.SetActive(false);
        }
        else if (args.interactableObject.transform.CompareTag("RightController"))
        {
            rightControllerModel.SetActive(false);
        }
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("LeftController"))
        {
            leftControllerModel.SetActive(true);
        }
        else if (args.interactableObject.transform.CompareTag("RightController"))
        {
            rightControllerModel.SetActive(true);
        }
    }
}