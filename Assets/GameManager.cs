using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentLevel = 0;
    public int lives = 5;
    public Text text;
    private int amountOfLevels = 2;
    private LevelManager levelManager = null;
    private List<GameObject> bricks = new List<GameObject>();
    private Ball ball = null;

    enum BrickType
    {
        OneHit = 0,
        TwoHit,
        ThreeHit
    };

    public GameObject oneHitBrick;
    public GameObject twoHitBrick;
    public GameObject threeHitBrick;

    public void ProcessBallLoss() {
        lives--;
        text.text = lives.ToString();
        if(lives <= 0) {
            levelManager.LoadLevel("Lose");
        } else {
            ball.startNewLevel();
        }
    }

    // Use this for initialization
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        ball = GameObject.FindObjectOfType<Ball>();
        LoadNextLevel();
        text.text = lives.ToString();
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyBrick(GameObject gameObject) {
        for (int i = 0; i < bricks.Count; ++i)
        {
            if (bricks[i].GetInstanceID() == gameObject.GetInstanceID())
            {
                bricks.RemoveAt(i);
                return;
            }
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevel >= amountOfLevels)
            levelManager.LoadLevel("Win");
        else
            LoadLevel(currentLevel);
    }

    private void LoadLevel(int levelNumber)
    {
        ball.startNewLevel();
        OneHitBlock.numberOfBricks = 0;
        currentLevel++;
        if (levelNumber == 0)
        {
            CreateCircle(1, twoHitBrick);
            CreateCircle(2, oneHitBrick);
        }
        else if (levelNumber == 1)
        {
            CreateCircle(3.5f, oneHitBrick);
            CreateCircle(3, twoHitBrick);
            CreateCircle(2, threeHitBrick);
        }
    }

    void CreateCircle(float radius, GameObject brickType)
    {
        float amount = Mathf.Floor(2 * Mathf.PI * radius);
        Vector3 screenCenter = new Vector3(Screen.width / 2.0f / Screen.width * 16.0f, Screen.height / 2.0f / Screen.height * 12.0f, 0.0f);
        float angle = 2.0f * Mathf.PI / amount;
        float delta = 0.0f;
        for(int i = 0; i < amount; ++i) {
            Vector3 newPos = new Vector3(radius * Mathf.Cos(delta), radius * Mathf.Sin(delta), 0.0f);
            Transform tr = Instantiate(brickType, screenCenter + newPos, Quaternion.identity).transform;
            Vector3 rotationVector = tr.position - screenCenter;
            tr.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), new Vector3(rotationVector.x, rotationVector.y, 0)) + 90);
            delta += angle;
        }
    }
}
