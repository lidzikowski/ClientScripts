using UnityEngine;

public class Player : Parent
{
    #region Identificators
    public string username { get; set; }
    #endregion

    #region Constructors
    public Player(TempPlayer player, ClientSocket _socket) :base(player, _socket)
    {
        id = player.id;
        username = player.username;
    }
    public Player(JsonPlayer js, ClientSocket _socket, GameObject prefab, Transform transform)
    {
        id = js.id;
        username = js.username;

        socket = _socket;

        ship_name = js.ship_name;

        hitpoints = js.hitpoints;
        hitpoints_max = js.hitpoints_max;

        shields = js.shields;
        shields_max = js.shields_max;

        speed = js.speed;

        position = new Vector3((float)js.position_x, (float)js.position_y, 0);
        new_position = new Vector3((float)js.new_position_x, (float)js.new_position_y, 0);

        createGameObject("PLAYER", id, prefab, transform);

        object_target = null;
        attack = false;
    }
    #endregion

    #region Method changes new position object
    /// <summary>
    /// Set new_position in object and calculate atan to rotate model
    /// </summary>
    public virtual void ChangePosition(float x, float y)
    {
        x = (float)System.Math.Round(x, 2);
        y = (float)System.Math.Round(y, 2);
        new_position = new Vector3(x, y, 0);
    }
    #endregion
}