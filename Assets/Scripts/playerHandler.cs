using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class playerHandler : MonoBehaviour
{
    Rigidbody rb;
    public GameObject fracturedObj;
    public Vector3 initialPos;
    private List<Vector3> cubePos;

    //PIVOT GAME OBJECTS
    public GameObject RIGHT;
    public GameObject LEFT;
    public GameObject CENTER;

    public Material indicatorColor;

    float speed = 0.0001f;
    float steps = 9;
    bool input = true;

    //FOR MOBILE
    private float screenHeight = Screen.height;
    private float screenWidth = Screen.width;
    private Vector2 startPos, endPos;
    private float swipeLimit = 50;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        initialPos = transform.position;
        cubePos = new List<Vector3>();

        string data = SaveSystem.Load("colorState");
        if (data != null && data != "{}")
        {
            shop_handler.PlayerColorHolder playerColors = JsonUtility.FromJson<shop_handler.PlayerColorHolder>(SaveSystem.Load("colorState"));
            transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = playerColors.GetColor("Indicator");
            transform.GetComponent<MeshRenderer>().material.color = playerColors.GetColor("Player");
        }
        //else
        //{
            
        //}
        transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = indicatorColor.color;

        //cubePos.Add(initialPos);
    }

    private void Update()
    {
        //PC_CONTROLS();
        MOBILE_CONTROLS();
        
    }
    void createObj() {
        if (!cubePos.Contains(initialPos))
        {
            cubePos.Add(initialPos);
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = initialPos;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            obj.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
            playerDeathHandler deathScript =  obj.AddComponent<playerDeathHandler>();
            deathScript.fracturedObj = fracturedObj;
            //deathScript.color = GetComponent<MeshRenderer>().material.color;
            obj.name = "Player";
            obj.tag = "Player";
        }        
    }
    
    public void deleteShapes() {
        cubePos.Clear();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players) {
            Destroy(p);
        }
    }
    
    private void PC_CONTROLS() {

        if (input == true)
        {
            if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 1)
            {
                //Build right
                transform.Translate(Vector3.right, transform.parent);
                createObj();
                initialPos = transform.position;
                CENTER.transform.position = transform.position;
            }
            if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -1)
            {
                //Build left
                transform.Translate(Vector3.left, transform.parent);
                createObj();
                initialPos = transform.position;
                CENTER.transform.position = transform.position;
            }
            if (Input.GetKeyDown(KeyCode.W) && transform.position.y < 2)
            {
                //Build up
                transform.Translate(Vector3.up, transform.parent);
                createObj();
                initialPos = transform.position;
            }
            if (Input.GetKeyDown(KeyCode.S) && transform.position.y > 0.5f)
            {
                //Build down
                transform.Translate(Vector3.down, transform.parent);
                createObj();
                initialPos = transform.position;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.y == 0.5f && transform.position.x < 1)
            {
                StartCoroutine("moveRight");
                input = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.y == 0.5f && transform.position.x > -1)
            {
                StartCoroutine("moveLeft");
                input = false;
            }
        }
    }
    private void MOBILE_CONTROLS() {
        if (Input.touchCount > 0 && UI_Handler.gameStarted && input) {
            Touch pos = Input.GetTouch(0);
            if (pos.phase == TouchPhase.Began)
            {
                startPos = pos.position;
                endPos = pos.position;
            }
            else if (pos.phase == TouchPhase.Moved) {
                endPos = pos.position;
            }
            else if (pos.phase == TouchPhase.Ended)
            {
                endPos = pos.position;
                checkSwipe(pos);
            }
        }
    }
    void checkSwipe(Touch pos)
    {
        //Check if Vertical swipe
        if (verticalMove() > swipeLimit && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (startPos.y - endPos.y < 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (startPos.y - endPos.y > 0)//Down swipe
            {
                OnSwipeDown();
            }
            startPos = endPos;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > swipeLimit && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (startPos.x - endPos.x < 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (startPos.x - endPos.x > 0)//Left swipe
            {
                OnSwipeLeft();
            }
            startPos = endPos;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("Less than screen width"+(pos.position.x < screenWidth/2).ToString());
            Debug.Log("Y POS" + transform.position.y.ToString());
            //Debug.Log("X POS" + (transform.position.x > -1).ToString());
            if (pos.position.x < screenWidth / 2 && Mathf.Round(transform.position.y*10)/10 == 0.5f && transform.position.x > -1)
            {
                input = false;
                //Debug.Log(input);
                StartCoroutine("moveLeft");
            }
            if (pos.position.x > screenWidth / 2 && Mathf.Round(transform.position.y * 10) / 10 == 0.5f && transform.position.x < 1)
            {
                input = false;
                //Debug.Log(input);
                StartCoroutine("moveRight");
            }
            //if (input == true) {
               
            //}
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(startPos.y - endPos.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(startPos.x - endPos.x);
    }
    void OnSwipeUp()
    {
        if (transform.position.y < 2)
        {
            //Build up
            transform.Translate(Vector3.up, transform.parent);
            createObj();
            initialPos = transform.position;
        }
    }
    void OnSwipeDown()
    {
        if (transform.position.y > 0.5f)
        {
            //Build down
            transform.Translate(Vector3.down, transform.parent);
            createObj();
            initialPos = transform.position;
        }
    }
    void OnSwipeLeft()
    {
        if (transform.position.x > -1)
        {
            //Build left
            transform.Translate(Vector3.left, transform.parent);
            createObj();
            initialPos = transform.position;
            CENTER.transform.position = transform.position;
        }
    }
    void OnSwipeRight()
    {
        if (transform.position.x < 1)
        {
            //Build right
            transform.Translate(Vector3.right,transform.parent);
            createObj();
            initialPos = transform.position;
            CENTER.transform.position = transform.position;
        }
    }
    IEnumerator moveLeft()
    {
        Debug.Log("Move left");
        for (int i = 0; i < (90 / steps); ++i)
        {
            transform.RotateAround(LEFT.transform.position, Vector3.forward, steps);
            yield return new WaitForSeconds(speed*Time.deltaTime);
        }
        CENTER.transform.position = transform.position;
        initialPos = transform.position;
        input = true;
        Debug.Log(input);
    }
    IEnumerator moveRight()
    {
        Debug.Log("Move right");
        for (int i = 0; i < (90 / steps); ++i)
        {
            transform.RotateAround(RIGHT.transform.position, Vector3.back, steps);
            yield return new WaitForSeconds(speed*Time.deltaTime);
        }
        initialPos = transform.position;
        CENTER.transform.position = transform.position;
        input = true;
        Debug.Log(input);
    }
}
