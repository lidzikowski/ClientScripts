using UnityEngine;
using System;

public class LocalPlayer : Parent
{
    #region Identificators
    public int user_id { get; set; }
    public string username { get; set; }
    #endregion

    #region Local parameters
    public string map_id { get; set; }
    public int scrap { get; set; }
    public int credits { get; set; }
    public int experience { get; set; }
    public int level { get; set; }
    public int ranking_points { get; set; }
    #endregion

    #region Equipments
    public string equipment { get; set; }
    public string equip_lasers { get; set; }
    public string equip_shields { get; set; }
    public string equip_engines { get; set; }
    public string equip_extras { get; set; }
    #endregion

    #region Constructors
    public LocalPlayer(TempLocalPlayer player, ClientSocket _socket) : base(player, _socket)
    {
        user_id = player.user_id;
        username = player.username;

        map_id = player.map_id;
        scrap = player.scrap;
        credits = player.credits;
        experience = player.experience;
        level = player.level;
        ranking_points = player.ranking_points;

        equipment = player.equipment;
        equip_lasers = player.equip_lasers;
        equip_shields = player.equip_shields;
        equip_engines = player.equip_engines;
        equip_extras = player.equip_extras;
    }
    #endregion
    
    #region Local player mouse controller
    public void ClickMouseController(ClientSocket socket)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (object_model == hit.transform.gameObject)
                return;
            else if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
                ;//selectTarget(hit.transform.gameObject);
        }
        else
        {
            float x = (Input.mousePosition.x - Screen.width / 2) / 20;
            float y = (Input.mousePosition.y - Screen.height / 2) / 20;
            ChangePosition(position.x + x, position.y + y);
        }
    }
    #endregion

    #region ChangePosition (send new position to server)
    /// <summary>
    /// Set new_position in object and calculate atan to rotate model
    /// </summary>
    public void ChangePosition(float x, float y)
    {
        x = (float)Math.Round(x, 2);
        y = (float)Math.Round(y, 2);
        new_position = new Vector3(x, y, 0);

        string json = "{\"method\":\"changePosition\", \"user_id\":\"" + user_id + "\", \"x\":" + x + ",\"y\":" + y + "}";
        socket.io.Emit("communication", json);
    }
    #endregion
}