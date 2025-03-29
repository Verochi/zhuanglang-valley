using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorymanager : MonoBehaviour
{
    public static inventorymanager instance { get; private set; }

    private Dictionary<itemtype, itemdata> itemdatadict = new Dictionary<itemtype, itemdata>();

    public inventorydata backpack;
    public BackpackDisplay backpackDisplay;

    public void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        itemdata[] itemdataarray = Resources.LoadAll<itemdata>("data");
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Seeds"); // ���� Seeds.png �е����о���

        foreach (itemdata data in itemdataarray)
        {
            itemdatadict.Add(data.type, data);
            Sprite targetSprite = System.Array.Find(spriteSheet, s => s.name == data.spriteName);
            if (targetSprite != null)
            {
                data.sprite = targetSprite;
            }
            else
            {
                Debug.LogWarning($"�޷��ҵ�����Ϊ {data.spriteName} �ľ���");
            }
        }
        backpack = Resources.Load<inventorydata>("backpack");
    }

    private itemdata GetItemdata(itemtype type)
    {
        itemdata data;
        bool success = itemdatadict.TryGetValue(type, out data);
        if (success)
            return data;
        else
        {
            Debug.LogWarning("δ�ҵ�����Ϊ" + type + "����Ʒ����");
            return null;
        }
    }

    public void AddToBackpack(itemtype type)
    {
        itemdata item = GetItemdata(type);
        if (item == null)
            return;
        foreach (slotdata slotdata in backpack.slotlist)
        {
            if (slotdata.item == item && slotdata.CanAddItem())
            {
                slotdata.addone();
                UpdateBackpackDisplay();
                return;
            }
        }
        foreach (slotdata slotdata1 in backpack.slotlist)
        {
            if (slotdata1.count == 0)
            {
                slotdata1.additem(item);
                UpdateBackpackDisplay();
                return;
            }
        }
        Debug.LogWarning("��������");
    }

    private void UpdateBackpackDisplay()
    {
        if (backpackDisplay != null)
        {
            backpackDisplay.UpdateBackpackDisplay();
        }
    }
}