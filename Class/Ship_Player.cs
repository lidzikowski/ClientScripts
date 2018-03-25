using UnityEngine;

public class Ship_Player : Ship
{
    public int required_level { get; set; }
    public int slots_lasers { get; set; }
    public int slots_shields { get; set; }
    public int slots_engines { get; set; }
    public int slots_extras { get; set; }

    public Ship_Player(T_Ship_Player ship, GameObject _model, Reward _reward) : base(ship, _model, _reward)
    {
        required_level = ship.required_level;
        slots_lasers = ship.slots_lasers;
        slots_shields = ship.slots_shields;
        slots_engines = ship.slots_engines;
        slots_extras = ship.slots_extras;
    }
}