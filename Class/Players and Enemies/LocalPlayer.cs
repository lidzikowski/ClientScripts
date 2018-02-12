using UnityEngine;
using System;
using System.Collections.Generic;

public class LocalPlayer : Parent
{
    #region Identificators
    public string username { get; set; }
    #endregion

    #region Local parameters
    public string map_id { get; set; }
    public double scrap { get; set; }
    public double credits { get; set; }
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


    public string ammunition { get; set; }

    private Sprite target_selected { get; set; }
    private Sprite target_no_selected { get; set; }

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

        ammunition = player.ammunition;

        equipment = player.equipment;
        equip_lasers = player.equip_lasers;
        equip_shields = player.equip_shields;
        equip_engines = player.equip_engines;
        equip_extras = player.equip_extras;

        target_selected = Resources.Load<Sprite>("Targets/selected");
        target_no_selected = Resources.Load<Sprite>("Targets/no_selected");
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
                    case "Player":
                        localPl.otherPlayers.players.TryGetValue(int.Parse(split[1]), out target);
                        break;
                    case "Enemie":
                        localPl.otherEnemies.enemies.TryGetValue(int.Parse(split[1]), out target);
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
        if (object_target != null)
        {
            object_target.enable_Bars(false, target_no_selected);
        }
        parent.enable_Bars(true, target_selected);

        attack = 0;
        object_target = parent;

        string json = "{\"method\":\"changeTarget\", \"id\":" + id + ", \"type\":\"" + object_target.GetType() + "\",\"target_id\":" + object_target.id + ", \"attack\":0}";
        socket.io.Emit("communication", json);
    }

    public void attackTarget()
    {
        if (object_target == null)
            return;

        if (attack == 1)
        {
            attack = 0;
            object_target.gameObject_selector.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else
        {
            attack = 1;
            object_target.gameObject_selector.GetComponent<SpriteRenderer>().color = Color.red;
        }

        string json = "{\"method\":\"changeTarget\", \"id\":" + id + ", \"type\":\"" + object_target.GetType() + "\",\"target_id\":" + object_target.id + ", \"attack\":" + attack + "}";
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

    public override void synchronize(Json pl, bool checkPosition = true)
    {
        base.synchronize(pl, checkPosition);

        JsonLocalPlayer lpl = pl as JsonLocalPlayer;
        map_id = lpl.map_id;
        scrap = lpl.scrap;
        credits = lpl.credits;
        experience = lpl.experience;
        level = lpl.level;
        ranking_points = lpl.ranking_points;
        ammunition = lpl.ammunition;
    }

    public override void DestroyThis(Dictionary<int, Parent> listEn, Dictionary<int, Parent> listPl, Parent local)
    {
        DisposeTarget(listEn, listPl, local);
        // Spawn effect destroy
    }

    public override void ChangeShip()
    {
        base.ChangeShip();

        gameObject_selector.transform.GetChild(2).GetComponent<TextMesh>().text = username;
    }
}