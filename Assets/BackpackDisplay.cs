using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BackpackDisplay : MonoBehaviour
{
    public Camera mainCamera;
    public Image backpackImage;
    public GameObject backpackSlotPrefab;
    public Transform slotParent;

    private List<Image> slotImages = new List<Image>();

    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = mainCamera;
        }

        RectTransform rectTransform = backpackImage.rectTransform;
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 0);
        rectTransform.pivot = new Vector2(0.5f, 0);
        rectTransform.anchoredPosition = new Vector2(0, 10);

        // 初始化 slotImages 列表
        for (int i = 0; i < inventorymanager.instance.backpack.slotlist.Count; i++)
        {
            GameObject slot = Instantiate(backpackSlotPrefab, slotParent);
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage == null)
            {
                Debug.LogError($"无法获取格子 {i} 的 Image 组件");
            }
            else
            {
                slotImages.Add(slotImage);
            }
        }

        UpdateBackpackDisplay();
    }

    public void UpdateBackpackDisplay()
    {
        for (int i = 0; i < inventorymanager.instance.backpack.slotlist.Count; i++)
        {
            slotdata slot = inventorymanager.instance.backpack.slotlist[i];
            if (slot.count > 0)
            {
                if (slot.item.sprite != null)
                {
                    Debug.Log($"设置物品槽 {i} 的精灵为: {slot.item.sprite.name}");
                    slotImages[i].sprite = slot.item.sprite;
                    slotImages[i].enabled = true;
                }
                else
                {
                    Debug.LogWarning($"背包第 {i} 个格子的物品精灵为空");
                }
            }
            else
            {
                slotImages[i].enabled = false;
            }
        }
    }
}