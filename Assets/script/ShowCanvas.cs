using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static staticValues;

public class ShowCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas; // 要控制的 Canvas 对象
    private XRRayInteractor rightController;
    public XRRayInteractor leftController;
    public Toggle toggle;
    private bool temp;

    public GameObject canvasUI;
    public GameObject vrCamera;
    public GameObject uiCamera;
    public GameObject vrConsumerCamera;
    public GameObject text;
    public GameObject check;

    private void Start()
    {
        rightController = GetComponent<XRRayInteractor>();
        leftController = GetComponent<XRRayInteractor>();
    }
    private void Update()
    {
        // 检测是否按下了 A 键
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && canvas.activeSelf == false)
        {
            canvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1) && canvas.activeSelf == true) {
            rightController.enabled = false;
            leftController.enabled = false;
            //Debug.Log(DeleteToggle.isOn);
            canvas.SetActive(false);
            rightController.enabled = true;
            leftController.enabled = true;
        }
    }

    public void gotoEditing()
    {
        canvasUI.SetActive(true);
        text.SetActive(true);
    }

    public void gotoConsumer()
    {
        check.SetActive(true);
    }

    public void yes() {
        check.SetActive(false);

        vrCamera.SetActive(false);
        vrConsumerCamera.SetActive(true);
        canvas.SetActive(false);
        DeleteToggle.isOn = false;
        SwapToggle.isOn = false;
        DuplicateToggle.isOn = false;

    }

    public void no() {
        check.SetActive(false);
    }
}
