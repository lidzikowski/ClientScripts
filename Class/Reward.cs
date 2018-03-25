using System.Collections.Generic;

public class Reward
{
    public double credits { get; set; }
    public double scrap { get; set; }
    public double experience { get; set; }
    public double ranking_points { get; set; }
    public List<Reward_Item> items { get; set; }
    
    public Reward(T_Reward reward, List<Reward_Item> _items)
    {
        credits = reward.credits;
        scrap = reward.scrap;
        experience = reward.scrap;
        ranking_points = reward.ranking_points;
        items = _items;
    }
}