using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }
    public List<ItemsCard> itemsPrefabList;
    private ItemsCard currentItem;
    public GameObject tempUI;
    public GameObject canvasUI;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemType itemtype) {
        if (currentItem != null) return;

        ItemsCard cardPrefab = GetItemsPrefeb(itemtype);
        if (cardPrefab == null) {
            Debug.Log(itemtype);
        }

        currentItem = Instantiate(cardPrefab);
        currentItem.transform.SetParent(tempUI.transform, false);

        Button bc = currentItem.GetComponent<Button>();
        Destroy(bc);
        Image img = currentItem.GetComponent<Image>();
        img.raycastTarget = false;
        currentItem.GetComponent<Transform>().localScale = new Vector3(0.64f, 0.64f, 1); 
    }

    private ItemsCard GetItemsPrefeb(ItemType itemsType) {

        foreach (ItemsCard card in itemsPrefabList) {
            if (card.itemType == itemsType) {
                return card;
            }
        }
        return null;
    }

    private void Update()
    {
        FollowCuesor();
        DeceteClick();
    }
    void FollowCuesor() {
        if (currentItem == null) return;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasUI.GetComponent<RectTransform>(),
            mousePosition,
            null,
            out localPoint);

        currentItem.GetComponent<RectTransform>().anchoredPosition = localPoint;
        
    }

    void DeceteClick() {
        if (Input.GetMouseButtonDown(1)) {
            if (currentItem == null) return;
            GameObject tempItem = tempUI.transform.GetChild(0).gameObject;
            Destroy(tempItem);
            currentItem = null;
        }
    }

    public ItemType OnBlockClick(BlockUI bui) {
        if (currentItem == null) return ItemType.None;

        ItemType it = currentItem.GetComponent<ItemsCard>().itemType;

        Image sourceImage = currentItem.GetComponent<Image>();
        bui.GetComponent<Image>().sprite = sourceImage.sprite;
        GameObject tempItem = tempUI.transform.GetChild(0).gameObject;
        Destroy(tempItem);
        currentItem = null;
        return it;
    }
}
