using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Obstacles;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleDatas[] obsDatas;
    [SerializeField]
    private GameObject[] spawnPoint;

    GameObject objObs;

    [SerializeField]
    private float timer = 6f;
    
    void Awake()
    {
        obsDatas = Resources.LoadAll("Database/Obstacle", typeof(ObstacleDatas)).Cast<ObstacleDatas>().ToArray();
    }

    void Start()
    {
        timer = Random.Range(3, 8);
    }

    // Update is called once per frame
    void Update()
    {

        if (timer <= 0)
        {
            int obsChooser = Random.Range(0, obsDatas.Length);
            int spawnChooser = Random.Range(0, spawnPoint.Length);

            objObs = Instantiate(obsDatas[obsChooser].itemModel.gameObject, spawnPoint[spawnChooser].transform.position, Quaternion.identity, spawnPoint[spawnChooser].transform);
            
            timer = Random.Range(3, 8);

            Destroy(objObs, 5);
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }
}
