using System.Collections.Generic;
using System.Linq;

public class Game_Data
{
    public Dictionary<int, Ammunition> ammunitions { get; set; }
    public Dictionary<int, Box> boxes { get; set; }
    public Dictionary<int, Item> items { get; set; }
    public Dictionary<int, Reward> rewards { get; set; }
    public List<Reward_Item> reward_items { get; set; }
    public Dictionary<int, Map> maps { get; set; }
    public Dictionary<int, Ship_Enemie> ships_enemie { get; set; }
    public Dictionary<int, Ship_Player> ships_players { get; set; }

    public Game_Data(Game_Resources resources, ServerData data)
    {
        ammunitions = new Dictionary<int, Ammunition>();
        foreach (T_Ammunition ammunition in data.ammunitions)
            ammunitions.Add(ammunition.ammunition_id, new Ammunition(ammunition, resources.ammunitions[ammunition.ammunition_id]));


        boxes = new Dictionary<int, Box>();
        foreach (T_Box box in data.boxes)
            boxes.Add(box.box_id, new Box(box, resources.boxes[box.box_id]));
        

        items = new Dictionary<int, Item>();
        foreach (T_Item item in data.items)
            items.Add(item.item_id, new Item(item));
        //items.Add(item.item_id, new Item(item, resources.items[item.item_id]));


        reward_items = new List<Reward_Item>();
        foreach (T_Reward_Item item in data.rewards_items)
            reward_items.Add(new Reward_Item(item, items[item.item_id]));


        rewards = new Dictionary<int, Reward>();
        foreach (T_Reward reward in data.rewards)
        {
            List<Reward_Item> items_list = reward_items.FindAll(o => o.reward_items_id == reward.reward_items_id);
            rewards.Add(reward.reward_id, new Reward(reward, items_list));
        }


        ships_enemie = new Dictionary<int, Ship_Enemie>();
        foreach (T_Ship_Enemie ship in data.ships_enemies)
            ships_enemie.Add(ship.ship_id, new Ship_Enemie(ship, resources.ships[ship.ship_id], rewards[ship.reward_id], ammunitions[ship.ammunition_id]));


        ships_players = new Dictionary<int, Ship_Player>();
        foreach (T_Ship_Player ship in data.ships_players)
            ships_players.Add(ship.ship_id, new Ship_Player(ship, resources.ships[ship.ship_id], rewards[ship.reward_id]));


        maps = new Dictionary<int, Map>();
        foreach (T_Map map in data.maps)
        {
            IEnumerable<Box> boxes_list = boxes.Values.Where(o => o.box_id == map.map_boxes_id);
            IEnumerable<Ship_Enemie> enemies_list = ships_enemie.Values.Where(o => o.ship_id == map.map_enemies_id);
            Map new_map = new Map(map);
            new_map.boxes_on_map.AddRange(boxes_list);
            new_map.enemies_on_map.AddRange(enemies_list);
            maps.Add(map.map_id, new_map);
        }
    }
}