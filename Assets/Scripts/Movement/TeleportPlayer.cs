﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeleportPlayer : NetworkBehaviour  
{
	[SerializeField]
	private Vector3 spawnCoords = new Vector3(-12,104,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		GameObject player = other.transform.parent.gameObject;
		Transform playerTransform = player.GetComponent<Transform>();
		if (playerTransform != null)
		{
			playerTransform.position = spawnCoords;
		}
	}
}
