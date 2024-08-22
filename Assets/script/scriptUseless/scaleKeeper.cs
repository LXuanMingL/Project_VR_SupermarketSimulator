using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleKeeper : MonoBehaviour
{
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.lossyScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            transform.localScale = new Vector3(
                originalScale.x / transform.parent.transform.lossyScale.x,
                originalScale.y / transform.parent.transform.lossyScale.y,
                originalScale.z / transform.parent.transform.lossyScale.z
            );
        }
    }
}
