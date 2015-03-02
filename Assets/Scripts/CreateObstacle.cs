using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateObstacle : MonoBehaviour
{
    public GameObject _obstacle, _player, test;
    bool _readyToSpawn = false;
    float _timeSinceLastSpawn = 0.0f;
    public List<GameObject> Trees;

	void Start ()
    {

	}
	
	void Update () 
    {
        GenerateObstacles();
        if(_readyToSpawn == false)
        {
            _timeSinceLastSpawn += Time.deltaTime;
        }
        if (_timeSinceLastSpawn >= Random.Range(.5f, 1.5f) && Time.time > 3)
        {
            _readyToSpawn = true;
            _timeSinceLastSpawn = 0;
        }

        

	}


    void GenerateObstacles()
    {
        int tempSpawn;
        tempSpawn = Random.Range(2, -2);
        if (tempSpawn < -0.5f)
            tempSpawn = -2;
        else if (tempSpawn > 1)
            tempSpawn = 2;
        else
            tempSpawn = 0;

        if (_readyToSpawn)
        {
            if (Trees.Count < 1)
            {
                for (int x = 0; x < 1; x++)
                {
                    Trees.Add(_obstacle);
                    Trees[x].transform.position = new Vector3(tempSpawn, 1.1f, _player.transform.position.z + 20);
                    Instantiate(Trees[x], Trees[x].transform.position, Quaternion.identity);
                    _readyToSpawn = false;
                    print("T = " + tempSpawn);
                }
            }
        }
    }
}
