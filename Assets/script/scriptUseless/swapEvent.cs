using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static staticValues;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class swapEvent : MonoBehaviour
{
    public Material hoverMaterial; // 悬停时要应用的材质
    public Material originalMaterialSwap;

    private Transform parentTransform;
    // 存储父对象的所有子对象
    private Transform[] siblings;

    private bool isHovering = false;

    void Update()
    {

        // 检查手柄的触发键输入
        if (isHovering && Input.GetKeyDown(KeyCode.JoystickButton15) && SwapToggle.isOn)
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
                    renderer.material = hoverMaterial;
                }
                else if (SwapToggle.swapObject2 == null) 
                {
                    SwapToggle.swapObject2 = parentTransform;
                    renderer = parentTransform.GetComponent<Renderer>();
                    renderer.enabled = true;
                    renderer.material = hoverMaterial;

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
        // 检查 Toggle 是否处于选中状态
        if (SwapToggle.isOn)
        {
            isHovering = true;
            
            if (transform.parent.tag == "Block")
            {
                Renderer renderer = transform.parent.GetComponent<Renderer>();
                renderer.material = hoverMaterial;
            }
        }
    }


    // 当悬停退出时调用
    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (SwapToggle.isOn)
        {
            isHovering = false;
            Renderer renderer = transform.parent.GetComponent<Renderer>();
            if (transform.parent.tag == "Block" && transform.parent != SwapToggle.swapObject1 && transform.parent != SwapToggle.swapObject2)
            {
                renderer.material = originalMaterialSwap;
            }
        }
    }
}
