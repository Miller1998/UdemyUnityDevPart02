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
    
    [Header("GameObject Caller")]
    private GameObject charPrefabs;
    
    #endregion

    #region Static_Variable

    internal static bool left = false;
    internal static bool right = false;

    #endregion

    void Awake()
    {

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

        if (playerHP == 0 || playerHP <= 0)//if player HP is at 0 or under it then game over
        {
            Destroy(charPrefabs);//destroy game obj
            Time.timeScale = 0;//stop the game
            //show GameOver UI

        }
        else
        {

        }

    }

    #region PlayerMovement

    void PlayerMovement()
    {

        //player moving up 
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
        mainCamera.transform.Translate(Vector3.up * Time.deltaTime * speed);
        obstacleSpawner.transform.Translate(Vector3.up * Time.deltaTime * speed);

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

                //instantiate character model inside this code place parent and it's position
                charPrefabs = (GameObject)Instantiate(characters[i].charModel, this.transform.position, Quaternion.identity, this.transform);
                            
            }

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
    }

    void OnCollisionEnter(Collision col)//collision = collision
    {
        
    }

    #endregion

}
