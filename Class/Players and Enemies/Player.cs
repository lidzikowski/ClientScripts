using UnityEngine;

public class Player : Parent
{
    #region Identificators
    public int user_id { get; set; }
    public string username { get; set; }
    #endregion

    #region Constructors
    public Player(TempPlayer player, ClientSocket _socket) :base(player, _socket)
    {
        user_id = player.user_id;
        username = player.username;
    }
    public Player(JsonPlayer js, ClientSocket _socket, GameObject playerPrefab, Transform playersTransform)
    {
        user_id = js.user_id;
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

        createGameObject(playerPrefab, playersTransform);

        object_target = null;
        attack = false;
    }
    #endregion

    private void createGameObject(GameObject playerPrefab, Transform playersTransform)
    {
        object_model = GameObject.Instantiate(playerPrefab, playersTransform);
        object_model.name = "PLAYER " + user_id;
        ChangeShip();
    }
}