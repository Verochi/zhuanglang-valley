using UnityEngine;

public enum itemtype
{
    wheatseed,
    potatoseed,
    None
}

[CreateAssetMenu()]
public class itemdata : ScriptableObject
{
    public itemtype type = itemtype.None;
    public string spriteName;
    public GameObject prefab;
    public int price;
    public int maxnum = 99;
    public Sprite sprite;
}