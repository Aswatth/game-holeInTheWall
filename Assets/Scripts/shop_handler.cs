using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shop_handler : MonoBehaviour
{
    public Text diamonds;
    private int diamondCount;

    public Material playerColor;
    public Material indicatorColor;

    int costOfColor = 25;

    [SerializeField] GameObject[] items;
    class SHOP {
        public string[] itemTags;
    }
    public class PlayerColorHolder
    {
        public float[] playerColor;
        public float[] indicatorColor;

        public PlayerColorHolder()
        {
            playerColor = new float[3];
            indicatorColor = new float[3];
        }
        public void SetColor(string type,Color color)
        {
            if (type == "Player")
            {
                playerColor[0] = color.r;
                playerColor[1] = color.g;
                playerColor[2] = color.b;
            }
            else
            {
                indicatorColor[0] = color.r;
                indicatorColor[1] = color.g;
                indicatorColor[2] = color.b;
            }
        }
        public Color GetColor(string type)
        {
            if (type == "Player")
                return new Color(playerColor[0], playerColor[1], playerColor[2],1);
            else
                return new Color(indicatorColor[0], indicatorColor[1], indicatorColor[2], 1);
        }
    }
    PlayerColorHolder playerColorHolder;
    SHOP shop;
    private void Start()
    {
        shop = new SHOP();
        playerColorHolder = new PlayerColorHolder();

        playerColorHolder.SetColor("Player", playerColor.color);
        playerColorHolder.SetColor("Indicator", indicatorColor.color);
        string color_data_to_save = JsonUtility.ToJson(playerColorHolder);
        SaveSystem.Save(color_data_to_save, "colorState");

        //PlayerPrefs.SetInt("HOLE_IN_THE_WALL_DIAMONDS", 0);
        diamondCount = PlayerPrefs.GetInt("HOLE_IN_THE_WALL_DIAMONDS", 0);
        diamonds.text = diamondCount.ToString();

        print(Application.dataPath);
        string data = SaveSystem.Load("shopState");

        if (data != null && data != "{}")
        {
            print(data);
            shop = JsonUtility.FromJson<SHOP>(SaveSystem.Load("shopState"));
            LoadItems(shop.itemTags);
        }
        else
        {
            shop.itemTags = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                shop.itemTags[i] = items[i].tag;
            }
        }
    }

    public void button_handler(GameObject button) {
        print(button.name);
        if (button.tag == "Locked")
        {
            if (diamondCount >= costOfColor)
            {
                diamondCount -= costOfColor;
                button.transform.GetChild(0).gameObject.SetActive(false);
                button.transform.GetChild(1).gameObject.SetActive(true);
                button.tag = "Bought";
            }
        }
        if (button.tag == "Equipped" || button.tag == "Bought") {
            GameObject prevEquipped = GameObject.FindGameObjectWithTag("Equipped");
            prevEquipped.tag = "Bought";
            prevEquipped.GetComponent<Outline>().effectColor = Color.black;
            editItem(shop, prevEquipped);

            button.tag = "Equipped";
            button.GetComponent<Outline>().effectColor = Color.green;
            editItem(shop, button);

            diamonds.text = diamondCount.ToString();

            //Setting player color
            GameObject colors = button.transform.GetChild(1).gameObject;

            playerColor.color = colors.GetComponent<Image>().color;
            indicatorColor.color = colors.transform.GetChild(0).gameObject.GetComponent<Image>().color;

            playerColorHolder.SetColor("Player", playerColor.color);
            playerColorHolder.SetColor("Indicator", indicatorColor.color);
        }
    }

    void editItem(SHOP shop, GameObject item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item) {
                shop.itemTags[i] = item.tag;
                break;
            }

        }
    }
    void LoadItems(string[] itemTag) {
        for (int i = 0; i < itemTag.Length; i++)
        {
            if (itemTag[i] == "Equipped")
            {
                items[i].transform.GetChild(0).gameObject.SetActive(false);
                items[i].transform.GetChild(1).gameObject.SetActive(true);
                items[i].GetComponent<Outline>().effectColor = Color.green;
            }
            else if (itemTag[i] == "Bought")
            {
                items[i].transform.GetChild(0).gameObject.SetActive(false);
                items[i].transform.GetChild(1).gameObject.SetActive(true);
                items[i].GetComponent<Outline>().effectColor = Color.black;
            }
            else if (itemTag[i] == "Locked") {
                items[i].transform.GetChild(0).gameObject.SetActive(true);
                items[i].transform.GetChild(1).gameObject.SetActive(false);
                items[i].GetComponent<Outline>().effectColor = Color.black;
            }
            items[i].tag = itemTag[i];
        }
    }

    public void back_to_game() {
        string data_to_save = JsonUtility.ToJson(shop);
        string color_data_to_save = JsonUtility.ToJson(playerColorHolder);
        print("DATA TO SAVE - " + data_to_save);
        print("Color DATA TO SAVE - " + color_data_to_save);
        PlayerPrefs.SetInt("HOLE_IN_THE_WALL_DIAMONDS", diamondCount);
        SaveSystem.Save(data_to_save,"shopState");
        SaveSystem.Save(color_data_to_save,"colorState");
        SceneManager.LoadScene("GameScene");
    }

}
