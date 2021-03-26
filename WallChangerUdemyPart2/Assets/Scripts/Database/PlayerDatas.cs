using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterDatas
{
    [CreateAssetMenu(fileName = "player", menuName = "Character/New_Character")]
    public class PlayerDatas : ScriptableObject
    {
        #region Player_Information

        [Header("Space Ship Info")]
        public string characterName;
        [TextArea(2,10)]
        public string charDesc;

        #endregion

        #region Player_Abilities

        [Header("Space Ship Abilities")]
        public int hp;
        public float movementSpeed;

        #endregion

        #region GameObject_Caller

        [Header("SpaceShip Model")]
        public GameObject charModel;
        //public AudioClip jumpSFX;
        public AudioClip destroyedSFX;

        #endregion
    }

}