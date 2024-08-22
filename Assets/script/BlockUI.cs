using System.Collections;
using System.Collections.Generic;
using TriLibCore;
using UnityEngine;
using UnityEngine.UIElements;
using TriLibCore.General;
using TriLibCore.Interfaces;
using TriLibCore.Utils;
using SFB;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using TriLibCore.Dae.Schema;
using Unity.VisualScripting; // Standalone File Browser

public class BlockUI : MonoBehaviour
{
    public GameObject blockIn3D;
    public List<ItemsPrefab> prefabIn3D;
    private ItemsPrefab currentItem;
    private GameObject tempObj;
    public float gap;
    private List<GameObject> comp = new List<GameObject>();

    public void click()
    {
        ItemType it = HandManager.Instance.OnBlockClick(this);
        if (it != ItemType.None && it != ItemType.outModel)
        {
            ItemsPrefab itemPrefab = GetItemsPrefeb(it);
            if (itemPrefab == null) return;

            foreach (Transform tf in blockIn3D.transform)
            {
                Destroy(tf.gameObject);
            }

            createItem(itemPrefab);
        } else if (it != ItemType.None && it == ItemType.outModel){

            RuntimeFBXLoader.Instance.OnModelLoaded += OnModelLoadedHandler;
            RuntimeFBXLoader.Instance.OpenFileBrowser();
        }
    }

    private void OnModelLoadedHandler(GameObject obj)
    {
        tempObj = RuntimeFBXLoader.Instance.getTempObj();

        foreach (Transform child in tempObj.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            { 
                addComponent(child.gameObject);
                comp.Add(child.gameObject);
            }
        }
        RuntimeFBXLoader.Instance.OnModelLoaded -= OnModelLoadedHandler;

        //CombineModelFragments();
    }

    private void CombineModelFragments()
    {
        if (comp == null || comp.Count < 1)
        {
            return;
        }


        // 给父对象添加一个Rigidbody组件，使其可以被抓取
        AddComponent<Rigidbody>(tempObj);
        tempObj.GetComponent<Rigidbody>().isKinematic = true;


        // 添加FixedJoint组件，将每个碎片连接到父对象
        foreach (GameObject fragment in comp)
        {
            Rigidbody fragmentRigidbody = fragment.GetComponent<Rigidbody>();

            FixedJoint fixedJoint = fragment.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = tempObj.GetComponent<Rigidbody>();
        }

    }

    private void addComponent(GameObject tempObj) {
        AddComponent<ItemsPrefab>(tempObj);
        tempObj.GetComponent<ItemsPrefab>().itemType = ItemType.outModel;

        AddComponent<MeshCollider>(tempObj);
        tempObj.GetComponent<MeshCollider>().convex = true;
        
        AddComponent<Rigidbody>(tempObj);
        
        AddComponent<XRGrabInteractable>(tempObj);
        
        AddComponent<interactEvent>(tempObj);
        tempObj.GetComponent<interactEvent>().hoverMaterialDelete = Resources.Load<Material>("Materials/HoverDelete");
        tempObj.GetComponent<interactEvent>().hoverMaterialDup = Resources.Load<Material>("Materials/HoverDuplicate");
        tempObj.GetComponent<interactEvent>().hoverMaterialSwap = Resources.Load<Material>("Materials/HoverBlock");
        tempObj.GetComponent<interactEvent>().originalMaterialSwap = Resources.Load<Material>("Materials/BlockDefault");

        tempObj.layer = 8;    
    }


    T AddComponent<T>(GameObject obj) where T : Component
    {
        T component = obj.AddComponent<T>();
        return component;
    }

    void createItem(ItemsPrefab itemPrefab) {

        // 获取父对象的边界
        Vector3 blockSize = blockIn3D.GetComponent<Renderer>().bounds.size;
        GameObject size = FindSiblingWithTag(blockIn3D);
        Vector3 blockSize1 = size.GetComponent<Renderer>().bounds.size;

        // 获取预制件的边界
        Vector3 itemSize = itemPrefab.GetComponent<Renderer>().bounds.size;

        // 计算需要创建的行和列数
        int columns = Mathf.FloorToInt((blockSize1.x + gap) / (itemSize.x + gap));
        int rows = Mathf.FloorToInt((blockSize1.z + gap) / (itemSize.z + gap));

        if (columns ==0) columns = 1;
        if (rows ==0) rows = 1;
        // 计算初始位置
        Vector3 startPosition = new Vector3(
            -blockSize.x / 2 + itemSize.x / 2 - ((blockSize1.x - blockSize.x)/2),
            -blockSize.y / 2 + itemSize.y / 2 - ((blockSize1.y - blockSize.y) / 2),
            -blockSize.z / 2 + itemSize.z / 2 - ((blockSize1.z - blockSize.z) / 2)
        );

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns ; j++)
            {
                // 计算实例化位置
                Vector3 position = new Vector3(
                    (startPosition.x + j * (itemSize.x + gap)) / blockSize.x,
                    startPosition.y,
                    (startPosition.z + i * (itemSize.z + gap)) / blockSize.z
                );
                // 实例化预制件并设置为父对象的子对象
                currentItem = Instantiate(itemPrefab);
                Rigidbody rb = currentItem.GetComponent<Rigidbody>();
                rb.interpolation = RigidbodyInterpolation.None;
                Vector3 originalScale = currentItem.transform.lossyScale;
                currentItem.transform.SetParent(blockIn3D.transform, false);
                currentItem.transform.localScale = new Vector3(
                    originalScale.x / blockIn3D.transform.lossyScale.x,
                    originalScale.y / blockIn3D.transform.lossyScale.y,
                    originalScale.z / blockIn3D.transform.lossyScale.z
                );
                currentItem.transform.localPosition = position;
                currentItem.transform.rotation = blockIn3D.transform.rotation;

                StartCoroutine(ReenableInterpolationAfterFrames(rb, RigidbodyInterpolation.Interpolate, 2));
            }
        }

        currentItem = null;
    }

    private IEnumerator ReenableInterpolationAfterFrames(Rigidbody rb, RigidbodyInterpolation mode, int frameCount)
    {
        // 等待指定的帧数
        for (int i = 0; i < frameCount; i++)
        {
            yield return null;
        }

        // 重新启用插值
        rb.interpolation = mode;
    }

    private ItemsPrefab GetItemsPrefeb(ItemType itemsType)
    {
        foreach (ItemsPrefab pb in prefabIn3D)
        {
            if (pb.itemType == itemsType)
            {
                return pb;
            }
        }
        return null;
    }

    GameObject FindSiblingWithTag(GameObject go)
    {
        string tag = "Size";

        Transform parent = go.transform.parent;

        if (parent == null)
        {
            return null;
        }

        foreach (Transform sibling in parent)
        {
            if (sibling.CompareTag(tag))
            {
                return sibling.gameObject;
            }
        }
        return null;
    }
}
