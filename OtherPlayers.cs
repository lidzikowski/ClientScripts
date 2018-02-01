using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayers : MonoBehaviour {

    private ClientSocket socket;
    public Dictionary<int, Parent> players;
    public Dictionary<int, Parent> enemiesReference;

    private GameObject prefab;

    void Start()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();

        prefab = Resources.Load<GameObject>("Prefabs/PLAYER");

        players = new Dictionary<int, Parent>();
        enemiesReference = GameObject.Find("OtherEnemies").GetComponent<OtherEnemies>().enemies;
    }

    private float timer = 0;

    void Update()
    {
        bool thisTime = timer >= 1f;
        foreach(Player player in players.Values)
        {
            player.FlyShip();
            if (thisTime)
            {
                if (player.lastUpdate < 0 || Vector3.Distance(socket.localPlayer.position, player.position) > 100)
                {
                    Destroy(player.object_model);
                    players.Remove(player.id);
                    return;
                }
                else
                    player.lastUpdate -= 1;
            }
        }
        if (thisTime)
            timer = 0;
        timer += Time.deltaTime;
    }

    public void arrayPlayers(List<JsonPlayer> _players)
    {
        foreach (JsonPlayer pl in _players) 
        {
            Parent player;
            if (players.TryGetValue(pl.id, out player))
                player.synchronize(pl, players, enemiesReference);
            else
                players.Add(pl.id, new Player(pl, socket, prefab, transform));
        }
    }
}