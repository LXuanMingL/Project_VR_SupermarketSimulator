using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static staticValues;

public class isOnCheckDuplicate : MonoBehaviour
{

    private Toggle toggle;
    public Toggle deleteToggle;
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
        DuplicateToggle.isOn = newValue;
        if (newValue) {
            deleteToggle.isOn = false;
            swapToggle.isOn = false;
        } 
    }
}
