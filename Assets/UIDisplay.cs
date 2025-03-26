using UnityEngine;
using UnityEngine.UI;
using System;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class UIDisplay : MonoBehaviour
{
    public Image portrait;
    public Text timetext;
    public Text moneytext;

    private float money = 1000f;
    private DateTime curtime = new DateTime(1, 1, 1);
    private float timer = 0f;

    public string spritename = "charactersheet_0";

    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("portrait/characterSheet");
        if (sprites.Length > 0)
        {
            //查找指定名称的切片
            Sprite targetsprite = System.Array.Find(sprites, s => s.name == spritename);
            portrait.sprite = targetsprite;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            curtime = curtime.AddDays(1);
            if (curtime.Day > 30)
            {
                curtime = new DateTime(curtime.Year, curtime.Month + 1, 1);
                if (curtime.Month > 12)
                    curtime = new DateTime(curtime.Year + 1, 1, 1);
            }
            timer = 0f;
        }

        timetext.text = curtime.ToString("yy年MM月dd日");
        moneytext.text = "资产: " + money.ToString("F2");
    }
}