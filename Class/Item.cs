using UnityEngine;

public class Item
{
    public string item_name { get; set; }
    public string item_type { get; set; }
    public string item_quality { get; set; }
    public int bonus { get; set; }
    public int value { get; set; }
    public int required_level { get; set; }
    GameObject model { get; set; }

    public Item(T_Item item)
    {
        item_name = item.item_name;
        item_type = item.item_type;
        item_quality = item.item_quality;
        bonus = item.bonus;
        value = item.value;
        required_level = item.required_level;
    }
    public Item(T_Item item, GameObject _model)
    {
        item_name = item.item_name;
        item_type = item.item_type;
        item_quality = item.item_quality;
        bonus = item.bonus;
        value = item.value;
        required_level = item.required_level;
        model = _model;
    }
}