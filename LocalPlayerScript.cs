using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnitySocketIO.Events;
using LitJson;
using System.Collections.Generic;

public class LocalPlayerScript : MonoBehaviour {

    private ClientSocket socket;

    private Transform LocalPlayer_BackgroundMap;
    public Transform LocalPlayer_OtherPlayers;
    public Transform LocalPlayer_OtherEnemies;

    private Text Interface_Minimap_Position;
    private Text Interface_User_Scrap;
    private Text Interface_User_Credits;
    private Text Interface_User_Experience;
    private Text Interface_User_Level;

    void Start ()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();
        
        CreateWorld();

        socket.io.On("synchronizeData", (SocketIOEvent e) => {
            JsonData jsonData = JsonMapper.ToObject<JsonData>(e.data);

            // Local player update
            socket.localPlayer.synchronize(jsonData.localPlayer, false);

            // Other player update
            LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().arrayPlayers(jsonData.otherPlayers);

            // Other enemie update
            LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().arrayEnemies(jsonData.otherEnemies);

            if (jsonData.objectDeaths.Count > 0)
                clearMap(jsonData.objectDeaths);
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
    }

    private void CreateWorld()
    {
        // Change local player ship
        socket.localPlayer.object_model = gameObject;
        socket.localPlayer.ChangeShip();

        // Change background
        LocalPlayer_BackgroundMap = GameObject.Find("Background").transform;
        ChangeMap();

        // Other player and enemies start object
        LocalPlayer_OtherPlayers = GameObject.Find("OtherPlayers").transform;
        LocalPlayer_OtherEnemies = GameObject.Find("OtherEnemies").transform;

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
        foreach (Transform t in LocalPlayer_BackgroundMap)
            Destroy(t.gameObject);
        Instantiate(socket.gameResources.maps[socket.localPlayer.map_id], LocalPlayer_BackgroundMap);
    }

    public void UpdateInterface()
    {
        Interface_Minimap_Position.text = string.Format("{0}   {1} / {2}", socket.localPlayer.map_id, Mathf.Round(socket.localPlayer.position.x), Mathf.Round(-socket.localPlayer.position.y));


        Interface_User_Scrap.text = socket.localPlayer.scrap.ToString();
        Interface_User_Credits.text = socket.localPlayer.credits.ToString();
        Interface_User_Experience.text = socket.localPlayer.experience.ToString();
        Interface_User_Level.text = socket.localPlayer.level.ToString();
    }
    
    private void clearMap(List<JsonDeath> list)
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

                        socket.localPlayer.DestroyThis(LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().enemies, LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().players);
                        socket.localPlayer.object_target = null;
                        socket.localPlayer.attack = 0;
                    }
                    else
                    {
                        Parent player;
                        if (LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().players.TryGetValue(obj.id, out player))
                        {
                            player.DestroyThis(LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().enemies, LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().players);
                            LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().players.Remove(player.id);
                        }
                    }
                    break;
                case "Enemie":
                    Parent enemie;
                    if (LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().enemies.TryGetValue(obj.id, out enemie))
                    {
                        enemie.DestroyThis(LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().enemies, LocalPlayer_OtherPlayers.GetComponent<OtherPlayers>().players);
                        LocalPlayer_OtherEnemies.GetComponent<OtherEnemies>().enemies.Remove(enemie.id);
                    }
                    break;
            }
        }
    }
}