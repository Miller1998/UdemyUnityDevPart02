using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterDatas;
using Obstacles;

public class Player : MonoBehaviour
{

    #region Public_Variable
    
    [Header("GameObject Caller")]
    public PlayerDatas[] characters;
    public ObstacleDatas[] obstacles;
    public GameObject mainCamera;
    public GameObject obstacleSpawner;

    [Header("Player Chooser")]
    public string chooseChar;

    [Header("PlayerArea")]
    public float xMin = -8f;
    public float xMax = 8f;

    #endregion

    #region Private_Variable

    [Header("PlayerCharacteristic")]
    [SerializeField]
    private string charName;
    [SerializeField]
    [Tooltip("Control Speed")]
    private float movementSpeed;
    [SerializeField]
    [Tooltip("Climbing Speed")]
    private float speed;
    [SerializeField]
    private int playerHP;
    [SerializeField]
    private int scoring;
    [SerializeField]
    private int timeRange;

    [Header("GameObject Caller")]
    private GameObject charPrefabs;
    public IngameGameManager gameManager;
    
    #endregion

    #region Static_Variable

    internal static bool left = false;
    internal static bool right = false;

    #endregion

    void Awake()
    {

        Time.timeScale = 1;
        scoring = 0;
        gameManager.timer = (float)timeRange;
        characters = Resources.LoadAll("Database/Character", typeof(PlayerDatas)).Cast<PlayerDatas>().ToArray();
        obstacles = Resources.LoadAll("Database/Obstacle", typeof(ObstacleDatas)).Cast<ObstacleDatas>().ToArray();
        //set the character (you can put it on the choosing character menu)
        PlayerPrefs.SetString("ChoosenOne", chooseChar);

    }

    void Start()
    {

        TheChoosenOne();

    }
    
    
    void Update()
    {

        PlayerMovement();
        // Scoring by timer

        NotTouchingWallTimer();

        if (playerHP <= 0)//if player HP is at 0 or under it then game over
        {
            Destroy(charPrefabs);//destroy game obj
            //Time.timeScale = 0;//stop the game
        }
        else
        {
            //scoring mode
            //sometimes this update can't be stopped by Time.timescale = 0 
            //so we must do this
            if (Time.timeScale == 0)
            {
                return;
            }
            else
            {
                scoring++;
                PlayerPrefs.SetInt("TotalScore", scoring);
            }
        }

    }

    #region PlayerMovement

    void PlayerMovement()
    {

        //player moving up 
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
        mainCamera.transform.Translate(Vector3.up * Time.deltaTime * speed);
        obstacleSpawner.transform.Translate(Vector3.up * Time.deltaTime * speed);

        //i hope we can change this to be the Mathf.Clamp
        if (left == true && this.transform.position.x >= xMin)
        {   
            this.transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        else if (right == true && this.transform.position.x <= xMax)
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }

    }

    #endregion

    #region PlayerController

    //Character Caller
    void TheChoosenOne()
    {
        charName = PlayerPrefs.GetString("ChoosenOne");

        for (int i = 0; i < characters.Length; i++)
        {

            if (charName.Contains(characters[i].characterName))
            {

                movementSpeed = characters[i].movementSpeed;
                playerHP = characters[i].hp;
                PlayerPrefs.SetInt("PlayerHP", playerHP);

                //instantiate character model inside this code place parent and it's position
                charPrefabs = (GameObject)Instantiate(characters[i].charModel, this.transform.position, Quaternion.identity, this.transform);
                            
            }

        }

    }

    void NotTouchingWallTimer()
    {
        gameManager.timer -= Time.deltaTime;

        if (gameManager.timer <= 0)
        {
            playerHP = 0;
            PlayerPrefs.SetInt("PlayerHP", playerHP);
        }
    }

    #endregion

    #region Trigger_and_Collider

    void OnTriggerEnter(Collider trigger)//Trigger = collider
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (trigger.tag == "Heal" && obstacles[i].itemName.Contains("Heal"))
            {
                
                playerHP = playerHP + obstacles[i].additionalHP;
                PlayerPrefs.SetInt("PlayerHP", playerHP);
                Debug.Log("Player HP = " + playerHP);
                
                Destroy(trigger.gameObject);

            }
            if (trigger.tag == "Enemy" && obstacles[i].itemName.Contains("Enemy"))
            {
                
                playerHP = playerHP + obstacles[i].additionalHP;
                PlayerPrefs.SetInt("PlayerHP", playerHP);
                Debug.Log("Player HP = " + playerHP);

                Destroy(trigger.gameObject);

            }
        }

        if (trigger.tag == "Wall")
        {
            gameManager.timer += (float)timeRange;
        }

        if (trigger.tag == "WallKiller")
        {
            playerHP = 0;
            PlayerPrefs.SetInt("PlayerHP", playerHP);
        }

    }

    void OnCollisionEnter(Collision col)//collision = collision
    {
        
    }

    #endregion

}
