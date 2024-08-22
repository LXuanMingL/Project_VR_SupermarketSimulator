using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static staticValues;

public class interactEvent : MonoBehaviour
{
    public Material hoverMaterialDelete; // 悬停时要应用的材质
    private Material originalMaterial; // 原始材质
    private MeshRenderer meshRenderer; // 物体的 Mesh Renderer 组件

    public Material hoverMaterialDup; // 悬停时要应用的材质
    private MeshRenderer meshRenderer111; // 物体的 Mesh Renderer 组件

    public Material hoverMaterialSwap; // 悬停时要应用的材质
    public Material originalMaterialSwap;

    private Transform parentTransform;
    // 存储父对象的所有子对象
    private Transform[] siblings;

    private bool isHovering = false;

    void Start()
    {
        // 获取物体的 Mesh Renderer 组件
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // 保存原始材质
            originalMaterial = meshRenderer.material;
        }

    }

    void Update()
    {
        // 检查手柄的触发键输入
        if (isHovering && Input.GetKeyDown(KeyCode.JoystickButton15) && DeleteToggle.isOn)
        {
            Destroy(gameObject);
        }else if (isHovering && Input.GetKeyDown(KeyCode.JoystickButton15) && DuplicateToggle.isOn)
        {
            meshRenderer.material = originalMaterial;
            // 复制游戏对象
            GameObject copiedObject = Instantiate(gameObject, transform.position + Vector3.up * transform.lossyScale.y, transform.rotation, gameObject.transform.parent);
            copiedObject.name = "CopyItem";
        }else if (isHovering && Input.GetKeyDown(KeyCode.JoystickButton15) && SwapToggle.isOn)
        {
            // 获取父对象
            parentTransform = transform.parent;
            Renderer renderer = null;


            if (SwapToggle.swapObject1 != parentTransform && SwapToggle.swapObject2 != parentTransform)
            {
                if (SwapToggle.swapObject1 == null)
                {
                    SwapToggle.swapObject1 = parentTransform;
                    renderer = parentTransform.GetComponent<Renderer>();
                    renderer.enabled = true;
                    renderer.material = hoverMaterialSwap;
                }
                else if (SwapToggle.swapObject2 == null)
                {
                    SwapToggle.swapObject2 = parentTransform;
                    renderer = parentTransform.GetComponent<Renderer>();
                    renderer.enabled = true;
                    renderer.material = hoverMaterialSwap;

                }
            }
            else
            {
                if (SwapToggle.swapObject1 == parentTransform)
                {
                    renderer = parentTransform.GetComponent<Renderer>();
                    SwapToggle.swapObject1 = null;
                    renderer.material = originalMaterialSwap;
                    renderer.enabled = false;
                }
                else if (SwapToggle.swapObject2 == parentTransform)
                {
                    renderer = parentTransform.GetComponent<Renderer>();
                    SwapToggle.swapObject2 = null;
                    renderer.material = originalMaterialSwap;
                    renderer.enabled = false;
                }
            }

            Debug.Log(SwapToggle.swapObject1 + ";" + SwapToggle.swapObject2 + ';' + parentTransform);

            if (SwapToggle.swapObject1 != null && SwapToggle.swapObject2 != null)
            {
                SwapToggle.swap();
                renderer = SwapToggle.swapObject1.GetComponent<Renderer>();
                renderer.material = originalMaterialSwap;
                renderer.enabled = false;
                renderer = SwapToggle.swapObject2.GetComponent<Renderer>();
                renderer.material = originalMaterialSwap;
                renderer.enabled = false;
                SwapToggle.swapObject1 = null;
                SwapToggle.swapObject2 = null;

            }
        }

    }

    private void OnEnable()
    {
        // 订阅 Ray Interactor 的 Hover 进入事件
        GetComponent<XRGrabInteractable>().hoverEntered.AddListener(OnHoverEntered);
        // 订阅 Ray Interactor 的 Hover 退出事件
        GetComponent<XRGrabInteractable>().hoverExited.AddListener(OnHoverExited);
    }

    private void OnDisable()
    {
        // 取消订阅 Ray Interactor 的 Hover 进入事件
        GetComponent<XRGrabInteractable>().hoverEntered.RemoveListener(OnHoverEntered);
        // 取消订阅 Ray Interactor 的 Hover 退出事件
        GetComponent<XRGrabInteractable>().hoverExited.RemoveListener(OnHoverExited);
    }

    // 当悬停进入时调用
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        isHovering = true; 
        // 检查 Toggle 是否处于选中状态
        if (DeleteToggle.isOn)
        {
            // 将物体的材质设置为悬停时的材质
            if (hoverMaterialDelete != null)
            {
                meshRenderer.material = hoverMaterialDelete;
            }
        }else if(DuplicateToggle.isOn)
        {
            // 将物体的材质设置为悬停时的材质
            if (hoverMaterialDup != null)
            {
                meshRenderer.material = hoverMaterialDup;
            }
        }else if(SwapToggle.isOn)
        {

            if (transform.parent.tag == "Block")
            {
                Renderer renderer = transform.parent.GetComponent<Renderer>();
                renderer.material = hoverMaterialSwap;
            }
        }

    }


    // 当悬停退出时调用
    private void OnHoverExited(HoverExitEventArgs args)
    {
        isHovering = false;
        // 恢复物体的原始材质
        if (DeleteToggle.isOn)
        {
            meshRenderer.material = originalMaterial;
        }else if (DuplicateToggle.isOn)
        {
            meshRenderer.material = originalMaterial;
        }else if (SwapToggle.isOn)
        {
            Renderer renderer = transform.parent.GetComponent<Renderer>();
            if (transform.parent.tag == "Block" && transform.parent != SwapToggle.swapObject1 && transform.parent != SwapToggle.swapObject2)
            {
                renderer.material = originalMaterialSwap;
            }
        }
    }
}
