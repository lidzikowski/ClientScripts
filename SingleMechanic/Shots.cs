using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shots : MonoBehaviour {

    private ClientSocket socket;
    private LocalPlayerScript lpScript;

    private List<Shot> shots;

    private GameObject damageMessage;
    private Dictionary<string, GameObject> Lasers;

    private OtherEnemies enemiesReference;
    private OtherPlayers playersReference;

    void Start ()
    {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();
        lpScript = GameObject.Find("LocalPlayer").GetComponent<LocalPlayerScript>();

        shots = new List<Shot>();

        damageMessage = Resources.Load<GameObject>("Prefabs/damage");

        Lasers = new Dictionary<string, GameObject>();
        Lasers.Add("laser0", Resources.Load<GameObject>("Lasers/laser0"));


        enemiesReference = GameObject.Find("OtherEnemies").GetComponent<OtherEnemies>();
        playersReference = GameObject.Find("OtherPlayers").GetComponent<OtherPlayers>();
    }

    void Update()
    {
        foreach(Shot shot in shots)
        {
            float distance = Vector2.Distance(shot.models[0].transform.position, shot.target_position);
            if (distance > 1)
                foreach (GameObject go in shot.models)
                    shot.moveObject();
            else
            {
                shot.createDamageMessage(damageMessage, transform);

                foreach (Parent parent in enemiesReference.enemies.Values)
                {
                    if (shot.attacker == parent)
                    {
                        parent.object_target = null;
                        parent.attack = 0;
                    }
                }
                foreach (Parent parent in playersReference.players.Values)
                {
                    if (shot.attacker == parent)
                    {
                        parent.object_target = null;
                        parent.attack = 0;
                    }
                }

                foreach (GameObject go in shot.models)
                    GameObject.Destroy(go);

                shots.Remove(shot);
                return;
            }
        }
    }

    public void createShots(List<JsonShot> list)
    {
        Parent attacker;
        Parent target;
        foreach (JsonShot obj in list)
        {
            attacker = null;
            switch (obj.attacker_type)
            {
                case "Player": // Player
                    if (socket.localPlayer.id == obj.attacker_id)
                        attacker = socket.localPlayer;
                    else
                        lpScript.otherPlayers.players.TryGetValue(obj.attacker_id, out attacker);
                    break;
                case "Enemie": // Enemie
                    lpScript.otherEnemies.enemies.TryGetValue(obj.attacker_id, out attacker);
                    break;
            }

            target = null;
            switch (obj.target_type)
            {
                case "Player": // in Player
                    if (socket.localPlayer.id == obj.target_id)
                        target = socket.localPlayer;
                    else
                        lpScript.otherPlayers.players.TryGetValue(obj.target_id, out target);
                    break;
                case "Enemie": // in Enemie
                    lpScript.otherEnemies.enemies.TryGetValue(obj.target_id, out target);
                    break;
            }

            if (attacker != null && target != null)
            {
                GameObject laser = Lasers["laser0"];
                Lasers.TryGetValue(obj.ammo_type, out laser);

                string damage;
                if (obj.damage > 0)
                    damage = obj.damage.ToString();
                else
                    damage = "MISS";

                Shot shot = new Shot(attacker, target, damage);
                shot.createShotObject(laser, transform);
                shots.Add(shot);
            }
        }
    }
}
