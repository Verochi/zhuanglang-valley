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
        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Seeds"); // 加载 Seeds.png 中的所有精灵

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
                Debug.LogWarning($"无法找到名称为 {data.spriteName} 的精灵");
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
            Debug.LogWarning("未找到类型为" + type + "的物品数据");
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
        Debug.LogWarning("背包已满");
    }

    private void UpdateBackpackDisplay()
    {
        if (backpackDisplay != null)
        {
            backpackDisplay.UpdateBackpackDisplay();
        }
    }
}