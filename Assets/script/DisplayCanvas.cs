using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCanvas : MonoBehaviour
{
    public RectTransform canvasRectTransform; // Canvas 的 RectTransform 组件
    public Transform cameraTransform; // 摄像头的 Transform 组件

    private Vector3 initialRelativePosition; // 初始相对位置
    private Quaternion initialRelativeRotation; // 初始相对旋转

    private void Awake()
    {
        // 记录初始时 Canvas 相对于摄像头的位置和旋转
        SetRelativePosition();
    }

    private void OnEnable()
    {
        // 在每次激活时重新设置 Canvas 的位置
        SetCanvasPosition();
    }

    private void SetRelativePosition()
    {
        // 计算初始时 Canvas 相对于摄像头的位置和旋转
        initialRelativePosition = cameraTransform.InverseTransformPoint(canvasRectTransform.position);
        initialRelativeRotation = Quaternion.Inverse(cameraTransform.rotation) * canvasRectTransform.rotation;
    }

    private void SetCanvasPosition()
    {
        // 计算实时相对位置和旋转
        Vector3 currentRelativePosition = cameraTransform.TransformPoint(initialRelativePosition);
        Quaternion currentRelativeRotation = cameraTransform.rotation * initialRelativeRotation;
        Quaternion finalRelativeRotation = Quaternion.Euler(currentRelativeRotation.eulerAngles.x, currentRelativeRotation.eulerAngles.y, 0f);

        // 更新 Canvas 的位置和旋转
        canvasRectTransform.position = currentRelativePosition;
        canvasRectTransform.rotation = finalRelativeRotation;
    }
}
