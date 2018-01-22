using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Parent : IFly, IHealth, ITarget
{
    public ClientSocket socket { get; set; }
    public float lastUpdate = 2;

    #region Ship
    public string ship_name { get; set; }
    #endregion

    #region IHealth variable
    public int hitpoints { get; set; }
    public int hitpoints_max { get; set; }
    public int shields { get; set; }
    public int shields_max { get; set; }
    #endregion

    #region IFly variable
    public int speed { get; set; }
    public Vector3 position { get; set; }
    public Vector3 new_position { get; set; }

    public GameObject object_model { get; set; }
    public GameObject object_ship_model { get; set; }
    protected List<GameObject> ship_engines { get; set; }
    protected float atan2 { get; set; }
    protected bool gearsUse { get; set; }
    protected bool gearsActive { get; set; }
    #endregion

    #region ITarget variable
    public Parent object_target { get; set; }
    public bool attack { get; set; }
    protected List<Transform> ship_lasers { get; set; }
    #endregion



    #region Constructors
    public Parent(Temp player, ClientSocket _socket)
    {
        socket = _socket;

        ship_name = player.ship_name;

        hitpoints = player.hitpoints;
        hitpoints_max = player.hitpoints_max;

        shields = player.shields;
        shields_max = player.shields_max;

        speed = player.speed;

        position = new Vector3(player.position_x / 100, player.position_y / 100, 0);
        new_position = position;

        object_target = null;
        attack = false;

        atan2 = 0;
        gearsUse = false;
        gearsActive = true;
    }
    public Parent()
    {
        atan2 = 0;
        gearsUse = false;
        gearsActive = true;
    }
    #endregion

    #region Change view model ship
    /// <summary>
    /// Select engines, lasers object and set start position GameObject in Unity
    /// </summary>
    public void ChangeShip()
    {
        // Check gameobject
        if (object_model == null)
            return;

        // Set ship object reference
        object_ship_model = object_model.transform.GetChild(0).gameObject;
        object_model.transform.position = position;

        // Remove all ship model and create new object ship
        foreach (Transform t in object_ship_model.transform)
            GameObject.Destroy(t);
        GameObject.Instantiate(socket.gameResources.ships[ship_name], object_ship_model.transform);

        // Select engines ship
        ship_engines = new List<GameObject>();
        foreach (Transform engine in object_ship_model.transform.GetChild(0).GetChild(0))
        {
            ship_engines.Add(engine.gameObject);
        }

        // Select lasers ship
        ship_lasers = new List<Transform>();
        foreach (Transform laser in object_ship_model.transform.GetChild(0).GetChild(1))
        {
            ship_lasers.Add(laser);
        }
    }
    #endregion

    #region IFly methods
    /// <summary>
    /// Set new_position in object and calculate atan to rotate model
    /// </summary>
    public virtual void ChangePosition(float x, float y)
    {
        x = (float)System.Math.Round(x, 2);
        y = (float)System.Math.Round(y, 2);
        new_position = new Vector3(x, y, 0);
    }

    /// <summary>
    /// Move GameObject and enable or disable engines if object moveing
    /// </summary>
    public virtual void FlyShip()
    {
        if (position != new_position)
        {
            object_model.transform.position = Vector3.MoveTowards(object_model.transform.position, new_position, Time.deltaTime * (speed / 15));
            position = object_model.transform.position;

            if (!gearsUse && !gearsActive)
                gearsUse = true;
        }
        else
        {
            if (gearsUse || gearsActive)
            {
                gearsUse = false;
                gearsActive = false;
                foreach (GameObject engine in ship_engines)
                    engine.SetActive(false);
            }
        }

        if (gearsUse && !gearsActive)
        {
            gearsActive = true;
            foreach (GameObject engine in ship_engines)
                engine.SetActive(true);
        }
    }

    /// <summary>
    /// Rotate ship object to atan2
    /// </summary>
    public virtual void RotateShip()
    {
        if (position == new_position)
            return;

        if (attack && object_target != null)
            atan2 = Mathf.Atan2(object_target.object_model.transform.position.y - position.y, object_target.object_model.transform.position.x - position.x) * Mathf.Rad2Deg + 90;
        else
            atan2 = Mathf.Atan2(new_position.y - position.y, new_position.x - position.x) * Mathf.Rad2Deg + 90;

        object_ship_model.transform.rotation = Quaternion.Lerp(object_ship_model.transform.rotation, Quaternion.Euler(0, 0, atan2), Time.deltaTime * 8);
    }
    
    protected void setPosition(Vector3 pos)
    {
        position = pos;
        object_model.transform.position = Vector3.MoveTowards(object_model.transform.position, pos, Time.deltaTime * (speed / 10));
    }
    #endregion

    /// <summary>
    /// Update object from server data (json to object)
    /// </summary>
    public virtual void synchronize(Json pl)
    {
        lastUpdate = 2;

        if (ship_name != pl.ship_name)
        {
            ship_name = pl.ship_name;
            ChangeShip();
        }

        hitpoints = pl.hitpoints;
        hitpoints_max = pl.hitpoints_max;
        shields = pl.shields;
        shields_max = pl.shields_max;

        Vector3 pos = new Vector3((float)pl.position_x, (float)pl.position_y, 0);
        if (Vector3.Distance(position, pos) > 1)
            setPosition(pos);
        new_position = new Vector3((float)pl.new_position_x, (float)pl.new_position_y, 0);

        speed = pl.speed;

        // Target

    }
}