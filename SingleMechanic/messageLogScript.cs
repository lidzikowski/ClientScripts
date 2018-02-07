using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageLogScript : MonoBehaviour {

    private float timer = 0;
	void Update () {
        timer += Time.deltaTime;
        if (timer > 5)
            Destroy(gameObject);
	}
}