using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;
using UnitySocketIO.Events;

public class ClientSocket : MonoBehaviour {

    public SocketIOController io;
    public bool socket_connected = false;

    public GameResources gameResources;
    public GameData gameData;

    public LocalPlayer localPlayer;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        changeScene("menu");
    }

	void Start () {
        io.Connect();
        io.On("connect", (SocketIOEvent e) => {
            socket_connected = true;
        });
        io.On("disconnect", (SocketIOEvent e) => {
            socket_connected = false;
        });
    }
    
    public void changeScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}