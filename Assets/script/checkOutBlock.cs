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

        // ��ӽ��봥����������
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
        // �Ƴ��뿪������������
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
        // �ȴ� 3 ��
        yield return new WaitForSeconds(3f);
        ExecuteNextStep();
    }

    void ExecuteNextStep()
    {
        checkOut();
    }

    public void checkOut() 
    {
        // ���֮ǰ�ļ���
        itemCount.Clear();

        // ����������ײ���壬�����ֵ��е�itemType����
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

        // ������
        foreach (KeyValuePair<ItemType, int> entry in itemCount)
        {
            Debug.Log($"{entry.Key}: {entry.Value}");
        }

        itemCount.Clear(); // ����ֵ��Ա��´�ʹ��
    }
}


