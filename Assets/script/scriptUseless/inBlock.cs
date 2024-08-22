using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 将进入的对象设置为这个游戏对象的子对象
        if (other.transform.parent == null || other.transform.parent.tag != "Block")
        {
            other.transform.SetParent(this.transform, true);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    other.transform.SetParent(null, true);
    //}

}
