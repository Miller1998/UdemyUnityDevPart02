using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    [CreateAssetMenu(fileName = "object", menuName = "Obstacle/object")]
    public class ObstacleDatas : ScriptableObject
    {

        #region Item_Information

        [Header("Item Info")]
        public string itemName;
        [TextArea(2, 10)]
        public string itemDesc;

        #endregion

        #region Item_Abilities

        [Header("Item Abilities")]
        public int additionalHP;
        public float movementSpeed;

        #endregion

        #region GameObject_Caller

        [Header("SpaceShip Model")]
        public GameObject itemModel;
        public AudioClip destroySFX;

        #endregion
    
    }

}