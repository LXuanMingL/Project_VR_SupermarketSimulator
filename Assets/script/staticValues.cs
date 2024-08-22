using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class staticValues : MonoBehaviour
{
    public static class DeleteToggle
    {
        public static bool isOn = false;
    }

    public static class DuplicateToggle
    {
        public static bool isOn = false;
    }

    public static class SwapToggle 
    {
        public static bool isOn = false;
        public static Transform swapObject1 = null;
        public static Transform swapObject2 = null;
        public static void swap() {

            if (swapObject1.tag == "Block" && swapObject2.tag == "Block") {
                // 交换两个父对象的世界位置
                Vector3 tempPosition = swapObject1.position;
                swapObject1.position = swapObject2.position;
                swapObject2.position = tempPosition;
            }
        }
    }

    //private static void SwapChildPosition(Transform child1, Transform child2)
    //{
    //    Vector3 tempPosition = child1.position;
    //    child1.position = child2.position;
    //    child2.position = tempPosition;
    //}

    //private static Vector3 CalculateCenterPosition(Transform parentTransform)
    //{
    //    // 获取父对象下的所有子对象
    //    Transform[] children = parentTransform.GetComponentsInChildren<Transform>();

    //    // 计算所有子对象的总位置
    //    Vector3 center = Vector3.zero;
    //    foreach (Transform child in children)
    //    {
    //        center += child.position;
    //    }

    //    // 将总位置除以子对象数量来获得平均位置，即中心位置
    //    center /= children.Length;

    //    return center;
    //}
}