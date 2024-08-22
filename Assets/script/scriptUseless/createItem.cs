using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createItem : MonoBehaviour
{
    public void createCube() {
        GameObject cubePrefab = Resources.Load<GameObject>("prefeb/Cube");

        GameObject mainCamera = GameObject.Find("XR Origin (XR Rig)/Camera Offset/Main Camera");
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 newPosition = cameraPosition + Vector3.forward * 0.5f;

        Transform cubeAllTransform = GameObject.Find("items/CubeAll").transform;

        GameObject cubeInstance = Instantiate(cubePrefab, newPosition, Quaternion.identity, cubeAllTransform);

        cubeInstance.name = "CreateItem";
    }

    public void createSphere()
    {
        GameObject cubePrefab = Resources.Load<GameObject>("prefeb/Sphere");

        GameObject mainCamera = GameObject.Find("XR Origin (XR Rig)/Camera Offset/Main Camera");
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 newPosition = cameraPosition + Vector3.forward * 0.5f;

        Transform cubeAllTransform = GameObject.Find("items/SphereAll").transform;

        GameObject cubeInstance = Instantiate(cubePrefab, newPosition, Quaternion.identity, cubeAllTransform);

        cubeInstance.name = "CreateItem";
    }
}
