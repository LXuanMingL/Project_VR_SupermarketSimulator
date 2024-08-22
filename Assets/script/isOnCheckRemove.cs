using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static staticValues;

public class isOnCheckDelete : MonoBehaviour
{

    private Toggle toggle;
    public Toggle duplicateToggle;
    public Toggle swapToggle;

    void Start()
    {
        toggle = GetComponentInChildren<Toggle>();

        // 添加监听 Toggle 的状态变化事件
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool newValue)
    {
        // 更新变量的值为 Toggle 的新状态
        DeleteToggle.isOn = newValue;

        if (newValue)
        {
            duplicateToggle.isOn = false;
            swapToggle.isOn = false;
        }
    }
}
