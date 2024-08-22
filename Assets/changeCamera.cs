using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCamera : MonoBehaviour
{
    public Camera cameraScreen;
    public Camera cameraVR;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            cameraVR.gameObject.SetActive(false);
            cameraScreen.gameObject.SetActive(true);
        }
    }
}
