using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;
using UnitySocketIO.Events;

public class ClientSocket : MonoBehaviour {

    public SocketIOController io;
    public bool socket_connected = false;

    public Game_Resources game_resources;
    public Game_Data game_data;
    public Player_Local player_local;

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

    public static ClientSocket Socket()
    {
        return GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();
    }
}