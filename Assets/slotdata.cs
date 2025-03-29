using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class slotdata
{
    public itemdata item;
    public int count = 0;
    public bool CanAddItem()
    {
        return count < item.maxnum;
    }

    public void addone()
    {
        count++;
    }

    public void additem(itemdata item)
    {
        this.item = item;
        count = 1;
    }
}
