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

        // ��ʼ�� slotImages �б�
        for (int i = 0; i < inventorymanager.instance.backpack.slotlist.Count; i++)
        {
            GameObject slot = Instantiate(backpackSlotPrefab, slotParent);
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage == null)
            {
                Debug.LogError($"�޷���ȡ���� {i} �� Image ���");
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
                    Debug.Log($"������Ʒ�� {i} �ľ���Ϊ: {slot.item.sprite.name}");
                    slotImages[i].sprite = slot.item.sprite;
                    slotImages[i].enabled = true;
                }
                else
                {
                    Debug.LogWarning($"������ {i} �����ӵ���Ʒ����Ϊ��");
                }
            }
            else
            {
                slotImages[i].enabled = false;
            }
        }
    }
}