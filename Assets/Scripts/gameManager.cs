using UnityEngine.UI;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public obstacleGenerator obj_gen;
    public GameObject player;
    
    public Material obstacleMaterial;

    GameObject obstacle;
    private float speed;
    private int range;

    private int scoreCount;
    private int highScoreCount;
    public Text score;
    public static int diamondScoreCount;

    private Vector3 player_initialPos;

    public static bool isGameOver;

    public GameObject gameOverScreen;
    public GameObject diamondUI;

    private void Start()
    {
        obstacle = obj_gen.createNewObstacle(obstacleMaterial, range);
        player_initialPos = player.transform.position;
        scoreCount = 0;
        diamondScoreCount = PlayerPrefs.GetInt("HOLE_IN_THE_WALL_DIAMONDS",0);
        highScoreCount = PlayerPrefs.GetInt("HOLE_IN_THE_WALL_HIGHSCORE", 0);

        speed = 5;
        range = 15;
        score.gameObject.SetActive(true);
        //player.SetActive(true);
        isGameOver = false;

    }

    private void Update()
    {
        obstacle.transform.position = new Vector3(obstacle.transform.position.x, 0, obstacle.transform.position.z - Time.deltaTime * speed);
        ColorChanger();
        if (obstacle.transform.position.z < -15)
        {
            Destroy(obstacle);
            player.GetComponent<playerHandler>().deleteShapes();
            if (obj_gen.checkShape())
            {
                print("Correct shape");
                speed = 7;
                scoreCount += 1;
                changeGameLogic();
                setScore();
            }
            else {
                print("Incorrect shape you cheated");
                setScore();
                saveScore();
                killPlayer();
            }
            player.GetComponent<playerHandler>().initialPos = player_initialPos;
            player.GetComponent<playerHandler>().CENTER.transform.position = player_initialPos;
            player.transform.position = player_initialPos;
            obstacle = obj_gen.createNewObstacle(obstacleMaterial,range);
        }
        if (isGameOver) {
            print("Enabling game over screen");
            gameOverScreen.SetActive(true);
            //this.gameObject.SetActive(false);
        }
    }

    private void ColorChanger()
    {
        Camera.main.backgroundColor = GameObject.Find("GameManager").GetComponent<ColorChanger>().playerColor;
    }

    private void killPlayer() {
        print("Killing for cheating");
        GameObject obj = GameObject.Find("Player");
        if (obj != null) {
            obj.GetComponent<playerDeathHandler>().killAll();
        }
        //obj_gen.enabled = false;
        gameObject.SetActive(false);
    }
    private void setScore() {
        score.text = scoreCount.ToString();
        if (scoreCount >= highScoreCount) {
            highScoreCount = scoreCount;
        }
        gameOverScreen.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = highScoreCount.ToString();
    }
    public void setDiamondCount() {
        Text temp_text = diamondUI.GetComponentInChildren<Text>();
        temp_text.text = diamondScoreCount.ToString();
    }
    void saveScore() {
        PlayerPrefs.SetInt("HOLE_IN_THE_WALL_DIAMONDS", diamondScoreCount);
        PlayerPrefs.SetInt("HOLE_IN_THE_WALL_HIGHSCORE", highScoreCount);
    }
    private void changeGameLogic()
    {
        if (scoreCount == 5)
            speed += 1f;
        if (scoreCount == 10)
        {
            range += 15;
            speed += 1f;
        }
        if (scoreCount == 15)
            speed += 1f;
        if (scoreCount == 20)
        {
            range += 15;
            speed += 1f;
        }
    }
}
