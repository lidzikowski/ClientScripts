using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnitySocketIO.Events;

public class LocalPlayerScript : MonoBehaviour {

    private ClientSocket socket;

    private Transform LocalPlayer_BackgroundMap;

    private float time = 1;
    private Text Interface_Minimap_Position;
    private Text Interface_User_Scrap;
    private Text Interface_User_Credits;
    private Text Interface_User_Experience;
    private Text Interface_User_Level;

    void Start ()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();
        
        CreateWorld();

        socket.io.On("communication", (SocketIOEvent e) => {

        });
    }

    void Update()
    {
        if (time > 1)
        {
            UpdateInterface();
            time = 0;
        }
        else if (time > 0.15)
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                socket.localPlayer.ClickMouseController(socket);
        }
        else
            time += Time.deltaTime;

        socket.localPlayer.FlyShip();
        socket.localPlayer.RotateShip();
        UpdateInterface();
    }

    private void CreateWorld()
    {
        // Change local player ship
        socket.localPlayer.object_model = gameObject;
        socket.localPlayer.ChangeShip(socket);

        // Change background
        LocalPlayer_BackgroundMap = GameObject.Find("Background").transform;
        ChangeMap();
        
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
            Destroy(t);
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
}