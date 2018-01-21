using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public int required_level { get; set; }
    public int slots_lasers { get; set; }
    public int slots_shields { get; set; }
    public int slots_engines { get; set; }
    public int slots_extras { get; set; }
}