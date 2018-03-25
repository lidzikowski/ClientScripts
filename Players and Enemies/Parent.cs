using System.Collections.Generic;
using UnityEngine;

public abstract class Parent
{
    public ClientSocket Socket { get; set; }
    public Ship_Player Ship { get; set; }
    public Map Map { get; set; }
    
    public int Hitpoints { get; set; }
    public int Hitpoints_Max { get; set; }
    public int Shields { get; set; }
    public int Shields_Max { get; set; }

    public Vector2 Position { get; set; }
    public Vector2 New_Position { get; set; }

    public Parent Object_Target { get; set; }

    public bool Attack { get; set; }

    public GameObject Model { get; set; }

    /*
    public GameObject object_model { get; set; }
    public GameObject object_ship_model { get; set; }
    protected List<GameObject> ship_engines { get; set; }
    protected float atan2 { get; set; }
    protected bool gearsUse { get; set; }
    protected bool gearsActive { get; set; }
    #endregion

    #region ITarget variable
    public Parent object_target { get; set; }
    public int attack { get; set; }
    public List<Transform> ship_lasers { get; set; }

    public GameObject gameObject_selector { get; set; }
    public Transform transform_hitpoints { get; set; }
    public Transform transform_shields { get; set; }
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

        position = new Vector3((float)player.position_x, (float)player.position_y, 0);
        new_position = position;

        object_target = null;
        attack = 0;

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


    public void enable_Bars(bool status, Sprite sprite)
    {
        transform_hitpoints.gameObject.SetActive(status);
        transform_shields.gameObject.SetActive(status);

        gameObject_selector.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject_selector.GetComponent<SpriteRenderer>().color = Color.grey;
    }

    public void update_Health_And_Shield_Bar()
    {
        if (hitpoints_max > 0)
        {
            float x = ((float)hitpoints / (float)hitpoints_max) * 128;
            if (x > 128)
                x = 128;
            transform_hitpoints.localScale = new Vector3(x, 4, 1);
        }
        else
            transform_hitpoints.localScale = new Vector3(0, 4, 1);

        if (shields_max > 0)
        {
            float x = ((float)hitpoints / (float)hitpoints_max) * 128;
            if (x > 128)
                x = 128;
            transform_shields.localScale = new Vector3(x, 4, 1);
        }
        else
            transform_shields.localScale = new Vector3(0, 4, 1);
    }


    #region Change view model ship
    /// <summary>
    /// Select engines, lasers object and set start position GameObject in Unity
    /// </summary>
    public virtual void ChangeShip()
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

        // Select statistics
        gameObject_selector = object_model.transform.GetChild(1).gameObject;
        transform_shields = gameObject_selector.transform.GetChild(0);
        transform_hitpoints = gameObject_selector.transform.GetChild(1);
    }
    #endregion

    #region IFly methods
    /// <summary>
    /// Move GameObject and enable or disable engines if object moveing
    /// </summary>
    public virtual void FlyShip()
    {
        RotateShip();
        if (position != new_position)
        {
            object_model.transform.position = Vector2.MoveTowards(object_model.transform.position, new_position, Time.deltaTime * (speed / 15));
            position = object_model.transform.position;

            if (!gearsUse && !gearsActive)
                gearsUse = true;
        }
        else
        {
            if (gearsUse || gearsActive)
            {
                gearsUse = false;
                changeEngine(false);
            }
        }

        if (gearsUse && !gearsActive)
            changeEngine(true);
    }

    /// <summary>
    /// Change status engine in ship model
    /// </summary>
    /// <param name="status"></param>
    protected void changeEngine(bool status)
    {
        gearsActive = status;
        foreach (GameObject engine in ship_engines)
            engine.SetActive(status);
    }

    /// <summary>
    /// Rotate ship object to atan2
    /// </summary>
    public virtual void RotateShip()
    {
        if (attack == 1 && object_target != null && object_target.object_model != null)
        {
            atan2 = Mathf.Atan2(object_target.object_model.transform.position.y - position.y, object_target.object_model.transform.position.x - position.x) * Mathf.Rad2Deg + 90;
        }
        else
        {
            float angle = Mathf.Atan2(new_position.y - position.y, new_position.x - position.x);
            if (angle == 0)
                return;
            atan2 = angle * Mathf.Rad2Deg + 90;
        }

        object_ship_model.transform.rotation = Quaternion.Lerp(object_ship_model.transform.rotation, Quaternion.Euler(0, 0, atan2), Time.deltaTime * 10);
    }

    protected void setPosition(Vector3 pos)
    {
        object_model.transform.position = Vector3.MoveTowards(object_model.transform.position, pos, Time.deltaTime * 500);
        position = object_model.transform.position;
    }
    #endregion

    #region Synchronize object and creator gameobject models
    /// <summary>
    /// Update object from server data (json to object)
    /// </summary>
    public virtual void synchronize(Json pl, bool checkPosition = true)
    {
        lastUpdate = 5;

        if (ship_name != pl.ship_name)
        {
            ship_name = pl.ship_name;
            ChangeShip();
        }

        hitpoints = pl.hitpoints;
        hitpoints_max = pl.hitpoints_max;
        shields = pl.shields;
        shields_max = pl.shields_max;

        if (checkPosition)
        {
            Vector3 pos = new Vector3((float)pl.position_x, (float)pl.position_y, 0);
            if (Vector3.Distance(position, pos) > 10)
                setPosition(pos);
            new_position = new Vector3((float)pl.new_position_x, (float)pl.new_position_y, 0);
        }

        speed = pl.speed;
    }
    public virtual void synchronize(Json pl, Dictionary<int, Parent> players, Dictionary<int, Parent> enemies)
    {
        synchronize(pl);
        if (pl.target_type != "" && pl.target_id > 0)
            switch (pl.target_type)
            {
                case "Player":
                    if (pl.target_id == socket.localPlayer.id)
                    {
                        object_target = socket.localPlayer;
                        attack = pl.attack;
                        return;
                    }
                    else
                    {
                        foreach (Parent parent in players.Values)
                        {
                            if (pl.target_id == parent.id)
                            {
                                object_target = parent;
                                attack = pl.attack;
                                return;
                            }
                        }
                    }
                    break;
                case "Enemie":
                    foreach (Parent parent in enemies.Values)
                    {
                        if (pl.target_id == parent.id)
                        {
                            object_target = parent;
                            attack = pl.attack;
                            return;
                        }
                    }
                    break;
            }
    }

    protected void createGameObject(string type, int id, GameObject prefab, Transform transform)
    {
        object_model = GameObject.Instantiate(prefab, transform);
        object_model.name = type + " " + id;
        ChangeShip();
    }
    #endregion

    #region Destroy method
    protected void DisposeTarget(Dictionary<int, Parent> listEn, Dictionary<int, Parent> listPl, Parent local)
    {
        if (local.object_target == this)
        {
            local.object_target = null;
            local.attack = 0;
        }
        else
        {
            foreach (Enemie en in listEn.Values)
            {
                if (en.object_target == this)
                {
                    en.object_target = null;
                    en.attack = 0;
                }
            }
            foreach (Player pl in listPl.Values)
            {
                if (pl.object_target == this)
                {
                    pl.object_target = null;
                    pl.attack = 0;
                }
            }
        }
    }

    public virtual void DestroyThis(Dictionary<int, Parent> listEn, Dictionary<int, Parent> listPl, Parent local)
    {
        DisposeTarget(listEn, listPl, local);
        // Spawn effect destroy
        GameObject.Destroy(object_model);
    }
    #endregion
    
    */
}