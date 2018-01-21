using System.Collections.Generic;
using UnityEngine;

public class GameResources
{
    public Dictionary<string, GameObject> ships { get; set; }
    public Dictionary<string, GameObject> maps { get; set; }
    public Dictionary<string, GameObject> targets { get; set; }
    public Dictionary<string, GameObject> effects { get; set; }

    public GameResources()
    {
        ships = new Dictionary<string, GameObject>();
        ships.Add("avalon", Resources.Load<GameObject>("Ships/avalon"));
        ships.Add("bravery", Resources.Load<GameObject>("Ships/bravery"));
        ships.Add("helios", Resources.Load<GameObject>("Ships/helios"));
        ships.Add("invictus", Resources.Load<GameObject>("Ships/invictus"));
        ships.Add("templar", Resources.Load<GameObject>("Ships/templar"));
        

        maps = new Dictionary<string, GameObject>();
        maps.Add("map0", Resources.Load<GameObject>("Maps/map0"));


        targets = new Dictionary<string, GameObject>();
        targets.Add("normal", Resources.Load<GameObject>("Targets/normal"));
        targets.Add("attack", Resources.Load<GameObject>("Targets/attack"));


        effects = new Dictionary<string, GameObject>();
        effects.Add("stars", Resources.Load<GameObject>("Effects/stars"));
    }
}