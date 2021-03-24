using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    public int durationWallInst = 40;

    [SerializeField]
    private GameObject[] wallPrefabs;
    [SerializeField]
    private Player player;
    [SerializeField]
    private int wallLength = 0;
    [SerializeField]
    public int timeDeletions = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallGravity();
    }

    void WallGravity()
    {
        int playerPos = (int)player.transform.position.y;

        if (playerPos % durationWallInst == 0)
        {
            GameObject newWall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], new Vector3(this.transform.position.x, wallLength,this.transform.position.z), Quaternion.identity, this.transform);
            wallLength += 80;
            Destroy(this.transform.GetChild(0).gameObject, timeDeletions);
        }
        
    }

}
