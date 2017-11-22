using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Paddle paddle;
    private Vector3 screenCenter;
    private bool gameStarted = false;

    private bool stickToPaddle = true;

    public void startNewLevel() {
        gameStarted = false;
        stickToPaddle = true;
    }

    private float paddleToBalldistance = 0.0f;
	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        screenCenter = new Vector3(Screen.width / 2.0f / Screen.width * 16.0f, Screen.height / 2.0f / Screen.height * 12.0f, 0.0f);
        paddleToBalldistance = GetComponent<CircleCollider2D>().radius + (paddle.GetComponent<BoxCollider2D>().size.y / 2);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameStarted)
            return;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update () {
        if (!gameStarted)
        {
            if (stickToPaddle)
            {
                Vector3 paddleToScreenCenterVector = screenCenter - paddle.transform.position;
                float scale = paddleToBalldistance / paddleToScreenCenterVector.magnitude;
                paddleToScreenCenterVector *= scale;
                this.transform.position = paddle.transform.position + paddleToScreenCenterVector;
            }
            if (Input.GetMouseButtonDown(0))
            {
                stickToPaddle = false;
                gameStarted = true;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, 5.0f);
            }
        }
	}
} 