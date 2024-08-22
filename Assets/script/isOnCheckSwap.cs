using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static staticValues;

public class isOnCheckSwap : MonoBehaviour
{
    private Toggle toggle;
    public Toggle duplicateToggle;
    public Toggle removeToggle;

    void Start()
    {
        toggle = GetComponentInChildren<Toggle>();

        // 添加监听 Toggle 的状态变化事件
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool newValue)
    {
        // 更新变量的值为 Toggle 的新状态
        SwapToggle.isOn = newValue;

        if (newValue)
        {
            duplicateToggle.isOn = false;
            removeToggle.isOn = false;
        }
    }
}
