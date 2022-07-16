using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{

    public Button playButton;
    public GameObject gameManager;
    public GameObject startUI;
    public GameObject EndUI;

    public GameObject diamondUI;

    public static bool gameStarted;


    private void Start()
    {
        gameStarted = false;
        Text diamondText = diamondUI.GetComponentInChildren<Text>();
        //SET DIAMOND COUNT
        diamondText.text = PlayerPrefs.GetInt("HOLE_IN_THE_WALL_DIAMONDS", 0).ToString();

        startUI.transform.GetChild(1).GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("HOLE_IN_THE_WALL_HIGHSCORE", 0).ToString();
    }
    public void startGame() {

        Debug.Log("Game started");
        GameObject.Find("Player").GetComponent<playerHandler>().enabled = true;
        startUI.SetActive(false);
        gameManager.SetActive(true);
        gameStarted = true;
    }

    public void Play() {
        playButton.transform.parent.gameObject.SetActive(false);
        gameManager.SetActive(true);
    }
    public void Replay() {

        //diamondUI.SetActive(true);

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void controls() {
        print("Controls pressed");
    }
    public void shop() {
        SceneManager.LoadScene("ShopScene");
    }
}
