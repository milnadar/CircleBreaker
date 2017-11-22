using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHitBlock : MonoBehaviour {

    public Sprite[] hitSprites;
    public AudioClip crack;

    private int timesHit = 0;
    private GameManager gameManager;

    public static int numberOfBricks = 0;
    //private bool isBreakable = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        ++timesHit;
        int maxHits = hitSprites.Length + 1;
        if (timesHit == maxHits) {
            numberOfBricks--;
            print("Number of bricks: " + numberOfBricks);
            if(numberOfBricks <= 0) {
                print("no blocks left");
                gameManager.LoadNextLevel();
            }
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    void LoadSprites() {
        int spriteIndex = timesHit - 1;
        this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    // Use this for initialization
    void Start () {
        //isBreakable = this.tag == "Breakable";
        numberOfBricks++;
        gameManager = GameObject.FindObjectOfType<GameManager>();
            print("Number of bricks: " + numberOfBricks);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
