using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayers : MonoBehaviour {

    private ClientSocket socket;
    public Dictionary<int, Player> players;

    private GameObject playerPrefab;

    void Start()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();

        playerPrefab = Resources.Load<GameObject>("Prefabs/PLAYER");

        players = new Dictionary<int, Player>();
    }

    private float timer = 0;

    void Update()
    {
        bool thisTime = timer >= 1f;
        foreach(Player player in players.Values)
        {
            player.FlyShip();
            player.RotateShip();
            if (thisTime)
            {
                player.lastUpdate -= 1;
                if (player.lastUpdate < 0 || Vector3.Distance(socket.localPlayer.position, player.position) >= 100)
                {
                    Destroy(player.object_model);
                    players.Remove(player.user_id);
                    return;
                }
            }
        }
        if (thisTime)
            timer = 0;
        timer += Time.deltaTime;
    }

    public void arrayPlayers(List<JsonPlayer> _players)
    {
        foreach(JsonPlayer pl in _players)
        {
            Player player;
            if(players.TryGetValue(pl.user_id, out player))
                player.synchronize(pl);
            else
                players.Add(pl.user_id, new Player(pl, socket, playerPrefab, transform));
        }
    }
}