using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterDatas;

public class Player : MonoBehaviour
{

    #region Public_Variable
    
    [Header("GameObject Caller")]
    public PlayerDatas[] characters;
    //public GameObject PlayerParent;

    [Header("Player Chooser")]
    public string chooseChar;

    #endregion

    #region Private_Variable

    [Header("PlayerCharacteristic")]
    [SerializeField]
    private string charName;
    [SerializeField]
    private float movementSpeed;
    [SerializeField][Tooltip("Climbing Speed")]
    private float speed;
    
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

    }

    #region PlayerMovement

    void PlayerMovement()
    {

        //player moving up 
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (left == true && this.transform.position.x >= -8f)
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        else if (right == true && this.transform.position.x <= 8f)
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

                //instantiate character model inside this code place parent and it's position
                charPrefabs = (GameObject)Instantiate(characters[i].charModel, this.transform.position, Quaternion.identity, this.transform);
                            
            }

        }

    }

    void PlayerController()
    {

    }

    #endregion

}
