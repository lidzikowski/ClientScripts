using UnityEngine;
using System;

public class LocalPlayer : Parent
{
    #region Identificators
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
        id = player.id;
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
    public void ClickMouseController(LocalPlayerScript localPl)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (object_model == hit.transform.gameObject)
                return;
            else if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
            {
                string[] split = hit.transform.name.Split();
                Parent target = null;

                switch(split[0])
                {
                    case "PLAYER":
                        localPl.LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().players.TryGetValue(int.Parse(split[1]), out target);
                        break;
                    case "ENEMY":
                        localPl.LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().enemies.TryGetValue(int.Parse(split[1]), out target);
                        break;
                }

                if (target != null && target != object_target)
                    selectTarget(target);
            }
        }
        else
        {
            float x = (Input.mousePosition.x - Screen.width / 2) / 20;
            float y = (Input.mousePosition.y - Screen.height / 2) / 20;
            ChangePosition(position.x + x, position.y + y);
        }
    }
    #endregion

    #region TargetAndAttack
    public void selectTarget(Parent parent)
    {
        object_target = parent;
        string json = "{\"method\":\"changeTarget\", \"id\":" + id + ", \"type\":\"" + parent.GetType() + "\",\"target_id\":" + parent.id + ", \"attack\":0}";
        socket.io.Emit("communication", json);
    }

    public void attackTarget()
    {
        if (object_target == null)
            return;

        int attackStatus = 1;
        if (attack)
        {
            attackStatus = 0;
            attack = false;
        }
        else
            attack = true;

        string json = "{\"method\":\"changeTarget\", \"id\":" + id + ", \"type\":\"" + object_target.GetType() + "\",\"target_id\":" + object_target.id + ", \"attack\":" + attackStatus + "}";
        Debug.Log(json);
        socket.io.Emit("communication", json);
    }
    #endregion

    #region ChangePosition (send new position to server)
    private float timerSynchronize = 0;
    /// <summary>
    /// Set new_position in object and calculate atan to rotate model
    /// </summary>
    public void ChangePosition(float x, float y)
    {
        x = (float)Math.Round(x, 2);
        y = (float)Math.Round(y, 2);
        new_position = new Vector3(x, y, 0);

        if (timerSynchronize > 0.1f)
        {
            string json = "{\"method\":\"changePosition\", \"id\":\"" + id + "\", \"x\":" + x + ",\"y\":" + y + "}";
            socket.io.Emit("communication", json);
            timerSynchronize = 0;
        }
        else
            timerSynchronize += Time.deltaTime;
    }
    #endregion
}