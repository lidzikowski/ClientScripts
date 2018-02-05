using UnityEngine;

public class Enemie : Parent
{
    #region local variables
    public string enemie_name { get; set; }
    public string enemie_type { get; set; }
    #endregion

    #region Constructors
    public Enemie(JsonEnemy js, ClientSocket _socket, GameObject prefab, Transform transform)
    {
        socket = _socket;

        id = js.id;
        enemie_name = js.enemie_name;
        enemie_type = js.enemie_type;

        ship_name = js.ship_name;

        hitpoints = js.hitpoints;
        hitpoints_max = js.hitpoints_max;

        shields = js.shields;
        shields_max = js.shields_max;

        speed = js.speed;

        position = new Vector3((float)js.position_x, (float)js.position_y, 0);
        new_position = new Vector3((float)js.new_position_x, (float)js.new_position_y, 0);

        createGameObject(GetType().ToString(), js.id, prefab, transform);

        object_target = null;
        attack = 0;
    }
    #endregion

    public override void ChangeShip()
    {
        base.ChangeShip();

        gameObject_selector.transform.GetChild(2).GetComponent<TextMesh>().text = string.Format("{0} {1}", enemie_type, enemie_name);
    }
}