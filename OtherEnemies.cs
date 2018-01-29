﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemies : MonoBehaviour
{
    private ClientSocket socket;
    public Dictionary<int, Parent> enemies;

    private GameObject prefab;

    void Start()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();

        prefab = Resources.Load<GameObject>("Prefabs/ENEMY");

        enemies = new Dictionary<int, Parent>();
    }

    private float timer = 0;

    void Update()
    {
        bool thisTime = timer >= 1f;
        foreach (Enemie enemie in enemies.Values)
        {
            enemie.FlyShip();
            if (thisTime)
            {
                if (enemie.lastUpdate < 0 || Vector3.Distance(socket.localPlayer.position, enemie.position) > 100)
                {
                    Destroy(enemie.object_model);
                    enemies.Remove(enemie.id);
                    return;
                }
                else
                    enemie.lastUpdate -= 1;
            }
        }
        if (thisTime)
            timer = 0;
        timer += Time.deltaTime;
    }

    public void arrayEnemies(List<JsonEnemy> _enemies)
    {
        foreach (JsonEnemy en in _enemies) 
        {
            Parent enemie;
            if (enemies.TryGetValue(en.id, out enemie))
                enemie.synchronize(en);
            else
                enemies.Add(en.id, new Enemie(en, socket, prefab, transform));
        }
    }
}