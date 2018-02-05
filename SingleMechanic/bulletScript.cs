using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    private float timer = 0;

	void Update () {
        timer += Time.deltaTime;

        transform.position += new Vector3(0.05f, 0.2f, 0);

        if (timer >= 0.5f)
            Destroy(gameObject);
	}
}