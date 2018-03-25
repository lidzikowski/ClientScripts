using System.Collections.Generic;
using UnityEngine;

public class Game_Resources
{
    public Dictionary<int, GameObject> ammunitions { get; set; }
    public Dictionary<int, GameObject> boxes { get; set; }
    public Dictionary<int, GameObject> items { get; set; }
    public Dictionary<int, GameObject> maps { get; set; }
    public Dictionary<int, GameObject> ships { get; set; }
    public Dictionary<string, GameObject> targets { get; set; }
    public Dictionary<string, GameObject> effects { get; set; }

    public Game_Resources()
    {
        ammunitions = new Dictionary<int, GameObject>();
        ammunitions.Add(1, Resources.Load<GameObject>("Lasers/laser0"));
        ammunitions.Add(2, Resources.Load<GameObject>("Lasers/laser1"));
        ammunitions.Add(3, Resources.Load<GameObject>("Lasers/laser2"));
        ammunitions.Add(4, Resources.Load<GameObject>("Lasers/laser3"));
        ammunitions.Add(5, Resources.Load<GameObject>("Lasers/laser4"));
        ammunitions.Add(6, Resources.Load<GameObject>("Lasers/laser5"));
        ammunitions.Add(7, Resources.Load<GameObject>("Lasers/laser6"));
        ammunitions.Add(8, Resources.Load<GameObject>("Lasers/laser7"));


        boxes = new Dictionary<int, GameObject>();
        boxes.Add(1, null);
        boxes.Add(2, null);
        boxes.Add(3, null);


        items = new Dictionary<int, GameObject>();
        items.Add(1, null);
        items.Add(2, null);
        items.Add(3, null);



        maps = new Dictionary<int, GameObject>();
        maps.Add(1, Resources.Load<GameObject>("Maps/map0"));
        maps.Add(2, Resources.Load<GameObject>("Maps/map1"));



        ships = new Dictionary<int, GameObject>();
        ships.Add(1, Resources.Load<GameObject>("Ships/Avalon"));
        ships.Add(2, Resources.Load<GameObject>("Ships/Templar"));
        ships.Add(3, Resources.Load<GameObject>("Ships/Invictus"));
        ships.Add(4, Resources.Load<GameObject>("Ships/Helios"));
        ships.Add(5, Resources.Load<GameObject>("Ships/Bravery"));

        

        targets = new Dictionary<string, GameObject>();
        targets.Add("normal", Resources.Load<GameObject>("Targets/normal"));
        targets.Add("attack", Resources.Load<GameObject>("Targets/attack"));



        effects = new Dictionary<string, GameObject>();
        effects.Add("stars", Resources.Load<GameObject>("Effects/stars"));
        

    }
}