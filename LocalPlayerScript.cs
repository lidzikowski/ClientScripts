using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnitySocketIO.Events;
using LitJson;
using System.Collections.Generic;

public class LocalPlayerScript : MonoBehaviour {

    private ClientSocket socket;


    private Transform backgroundMap;
    public OtherPlayers otherPlayers;
    public OtherEnemies otherEnemies;
    public Shots shots;


    private Text Interface_Minimap_Position;
    private Text Interface_User_Scrap;
    private Text Interface_User_Credits;
    private Text Interface_User_Experience;
    private Text Interface_User_Level;

    void Start ()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();
        shots = GameObject.Find("Shots").GetComponent<Shots>();

        CreateWorld();

        socket.io.On("synchronizeData", (SocketIOEvent e) => {
            JsonData jsonData = JsonMapper.ToObject<JsonData>(e.data);

            // Local player update
            socket.localPlayer.synchronize(jsonData.localPlayer, false);

            // Other player update
            otherPlayers.arrayPlayers(jsonData.otherPlayers);

            // Other enemie update
            otherEnemies.arrayEnemies(jsonData.otherEnemies);

            // Create deaths
            if (jsonData.objectDeaths.Count > 0)
                createDeaths(jsonData.objectDeaths);

            // Create shots
            if (jsonData.shots.Count > 0)
                shots.createShots(jsonData.shots);
        });
    }

    void Update()
    {
        UpdateInterface();

        // Change position function
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            socket.localPlayer.ClickMouseController(this);
        socket.localPlayer.FlyShip();

        // Attack target function
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            socket.localPlayer.attackTarget();

        if (socket.localPlayer.object_target != null)
            socket.localPlayer.object_target.update_Health_And_Shield_Bar();
    }

    private void CreateWorld()
    {
        // Change local player ship
        socket.localPlayer.object_model = gameObject;
        socket.localPlayer.ChangeShip();

        // Change background
        backgroundMap = GameObject.Find("Background").transform;
        ChangeMap();

        // Other player and enemies start object
        otherPlayers = GameObject.Find("OtherPlayers").GetComponent<OtherPlayers>();
        otherEnemies = GameObject.Find("OtherEnemies").GetComponent<OtherEnemies>();

        // Interface local player
        Transform userInterface = GameObject.Find("Interface").transform;
        foreach (Transform transform in userInterface)
        {
            switch (transform.name)
            {
                case "minimap":
                    foreach (Transform child in transform)
                        if (child.name == "position") Interface_Minimap_Position = child.GetComponent<Text>();
                    break;
                case "user_bar":
                    foreach (Transform child in transform)
                        if (child.name == "user")
                            foreach (Transform t in child)
                            {
                                switch (t.name)
                                {
                                    case "scrap":
                                        Interface_User_Scrap = t.GetComponent<Text>();
                                        break;
                                    case "credits":
                                        Interface_User_Credits = t.GetComponent<Text>();
                                        break;
                                    case "experience":
                                        Interface_User_Experience = t.GetComponent<Text>();
                                        break;
                                    case "level":
                                        Interface_User_Level = t.GetComponent<Text>();
                                        break;
                                }
                            }
                    break;
            }
        }
    }

    public void ChangeMap()
    {
        foreach (Transform t in backgroundMap)
            Destroy(t.gameObject);
        Instantiate(socket.gameResources.maps[socket.localPlayer.map_id], backgroundMap);
    }

    public void UpdateInterface()
    {
        Interface_Minimap_Position.text = string.Format("{0}   {1} / {2}", socket.localPlayer.map_id, Mathf.Round(socket.localPlayer.position.x), Mathf.Round(-socket.localPlayer.position.y));


        Interface_User_Scrap.text = socket.localPlayer.scrap.ToString();
        Interface_User_Credits.text = socket.localPlayer.credits.ToString();
        Interface_User_Experience.text = socket.localPlayer.experience.ToString();
        Interface_User_Level.text = socket.localPlayer.level.ToString();

        socket.localPlayer.update_Health_And_Shield_Bar();
    }
    
    private void createDeaths(List<JsonDeath> list)
    {
        foreach (JsonDeath obj in list)
        {
            switch (obj.type)
            {
                case "Player":
                    if (obj.id == socket.localPlayer.id)
                    {
                        socket.localPlayer.map_id = "map0";
                        socket.localPlayer.position = new Vector2(50.5f, -50.5f);
                        socket.localPlayer.object_model.transform.position = socket.localPlayer.position;
                        socket.localPlayer.new_position = socket.localPlayer.position;
                        ChangeMap();

                        socket.localPlayer.DestroyThis(otherEnemies.enemies, otherPlayers.players, socket.localPlayer);
                        socket.localPlayer.object_target = null;
                        socket.localPlayer.attack = 0;
                    }
                    else
                    {
                        Parent player;
                        if (otherPlayers.players.TryGetValue(obj.id, out player))
                        {
                            player.DestroyThis(otherEnemies.enemies, otherPlayers.players, socket.localPlayer);
                            otherPlayers.players.Remove(player.id);
                        }
                    }
                    break;
                case "Enemie":
                    Parent enemie;
                    if (otherEnemies.enemies.TryGetValue(obj.id, out enemie))
                    {
                        enemie.DestroyThis(otherEnemies.enemies, otherPlayers.players, socket.localPlayer);
                        otherEnemies.enemies.Remove(enemie.id);
                    }
                    break;
            }
        }
    }
}