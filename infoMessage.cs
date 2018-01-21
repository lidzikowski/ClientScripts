using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoMessage : MonoBehaviour {

    private float time = 0f;

    void Update ()
    {
        time += Time.deltaTime;
        if (time > 5)
        {
            Destroy(gameObject);
        }
    }
}