using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkOutBlock : MonoBehaviour
{

    Dictionary<ItemType, int> itemCount = new Dictionary<ItemType, int>();
    HashSet<Collider> collidingObjects = new HashSet<Collider>();


    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent != null && other.transform.parent.GetComponent<ItemsPrefab>() != null && other.transform.parent.GetComponent<ItemsPrefab>().itemType == ItemType.shoppingBasket) {
            return; 
        }

        // 添加进入触发器的物体
        if (other.GetComponent<ItemsPrefab>() != null)
        {
            collidingObjects.Add(other);
        }

        if (other.GetComponent<ItemsPrefab>() != null && other.GetComponent<ItemsPrefab>().itemType == ItemType.shoppingBasket) {
            StartCoroutine(WaitAndExecute());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 移除离开触发器的物体
        if (collidingObjects.Contains(other))
        {
            collidingObjects.Remove(other);
        }
    }

    private void CheckItemType(Collider other)
    {
        ItemsPrefab item = other.GetComponent<ItemsPrefab>();
        if (item != null)
        {
            Debug.Log($"Colliding ItemType: {item.itemType}");
        }
    }

    IEnumerator WaitAndExecute()
    {
        // 等待 3 秒
        yield return new WaitForSeconds(3f);
        ExecuteNextStep();
    }

    void ExecuteNextStep()
    {
        checkOut();
    }

    public void checkOut() 
    {
        // 清除之前的计数
        itemCount.Clear();

        // 遍历所有碰撞物体，更新字典中的itemType数量
        foreach (Collider collider in collidingObjects)
        {
            ItemsPrefab item = collider.GetComponent<ItemsPrefab>();
            if (item != null)
            {
                ItemType type = item.itemType;
                if (itemCount.ContainsKey(type))
                {
                    itemCount[type]++;
                }
                else
                {
                    itemCount[type] = 1;
                }
            }
        }

        // 输出结果
        foreach (KeyValuePair<ItemType, int> entry in itemCount)
        {
            Debug.Log($"{entry.Key}: {entry.Value}");
        }

        itemCount.Clear(); // 清除字典以备下次使用
    }
}


