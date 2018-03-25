using System.Collections.Generic;
using UnityEngine;

public class Shot
{
    /*
    public Parent attacker { get; set; }
    public Parent target { get; set; }
    public List<GameObject> models { get; set; }
    public Vector3 target_position { get; set; }
    public string damage { get; set; }

    public Shot(Parent _attacker, Parent _target, string _damage)
    {
        attacker = _attacker;
        target = _target;
        damage = _damage;
        if (_damage == "MISS")
            target_position = RandomCircle(_target.position, Random.Range(1, 50) / 10);
        else
            target_position = _target.position;
        models = new List<GameObject>();
    }

    public void createShotObject(GameObject _object, Transform _transform)
    {
        for (int i = 0; i < attacker.ship_lasers.Count; i++)
        {
            GameObject go = GameObject.Instantiate(_object, _transform);
            go.transform.position = attacker.ship_lasers[i].transform.position;
            models.Add(go);
        }
    }

    public void moveObject()
    {
        if (target != null)
            target_position = target.position;

        foreach (GameObject go in models)
        {
            go.transform.position = Vector3.MoveTowards(go.transform.position, target_position, Time.deltaTime * 100);
            Vector3 dir = go.transform.position - target_position;
            go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90));
        }
    }

    public void createDamageMessage(GameObject _message, Transform _transform)
    {
        GameObject dmgMessage = GameObject.Instantiate(_message, _transform);
        dmgMessage.transform.position = target.position;
        dmgMessage.GetComponent<TextMesh>().text = damage;
    }

    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    */
}