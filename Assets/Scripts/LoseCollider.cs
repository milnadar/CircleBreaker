using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

    private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player")
            return;
        gameManager.ProcessBallLoss();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
    }

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
