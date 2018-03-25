
public class Reward_Item
{
    public Item item { get; set; }
    public int reward_items_id { get; set; }
    public int upgrade_level { get; set; }
    public int chance { get; set; }
    
    public Reward_Item(T_Reward_Item reward, Item _item)
    {
        item = _item;
        reward_items_id = reward.reward_items_id;
        upgrade_level = reward.upgrade_level;
        chance = reward.chance;
    }
}