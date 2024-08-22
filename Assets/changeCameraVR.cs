using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class changeCameraVR : MonoBehaviour
{

    public Camera cameraScreen;
    public Camera cameraVR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            cameraVR.gameObject.SetActive(true);
            cameraScreen.gameObject.SetActive(false);
        }
    }
}
