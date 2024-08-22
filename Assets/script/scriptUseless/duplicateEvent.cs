using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static staticValues;

public class duplicateEvent : MonoBehaviour
{
    public Material hoverMaterialDup; // 悬停时要应用的材质

    private Material originalMaterialDup; // 原始材质
    private MeshRenderer meshRendererDup; // 物体的 Mesh Renderer 组件
    private bool isHovering = false;

    void Start()
    {
        // 获取物体的 Mesh Renderer 组件
        meshRendererDup = GetComponent<MeshRenderer>();
        if (meshRendererDup != null)
        {
            // 保存原始材质
            originalMaterialDup = meshRendererDup.material;
        }
    }

    void Update()
    {
        // 检查手柄的触发键输入
        if (isHovering && Input.GetKeyDown(KeyCode.JoystickButton15) && DuplicateToggle.isOn)
        {

            meshRendererDup.material = originalMaterialDup;
            // 复制游戏对象
            GameObject copiedObject = Instantiate(gameObject, transform.position + Vector3.up*0.3f, transform.rotation);
            MeshRenderer copiedMeshRenderer = copiedObject.GetComponent<MeshRenderer>();
            copiedObject.name ="CopyItem";
        }
    }

    private void OnEnable()
    {
        // 订阅 Ray Interactor 的 Hover 进入事件
        GetComponent<XRGrabInteractable>().hoverEntered.AddListener(OnHoverEntered);
        // 订阅 Ray Interactor 的 Hover 退出事件
        GetComponent<XRGrabInteractable>().hoverExited.AddListener(OnHoverExited);
    }


    // 当悬停进入时调用
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // 检查 Toggle 是否处于选中状态
        if (DuplicateToggle.isOn)
        {
            isHovering = true;
            // 将物体的材质设置为悬停时的材质
            if (hoverMaterialDup != null)
            {
                meshRendererDup.material = hoverMaterialDup;
            }
        }
    }


    // 当悬停退出时调用
    private void OnHoverExited(HoverExitEventArgs args)
    {
        isHovering = false;
        // 恢复物体的原始材质
        if (DuplicateToggle.isOn)
        {
            meshRendererDup.material = originalMaterialDup;
        }
    }
}
