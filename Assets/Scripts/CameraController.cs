using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject player;
    private Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        var newPos = new Vector3()
        {
            x = player.transform.position.x + offset.x,
            y = player.transform.position.y + offset.y,
            z = player.transform.position.z + offset.z
        };

        transform.position = Vector3.Lerp(transform.position, newPos, 5.0f * Time.deltaTime);
	}
}
