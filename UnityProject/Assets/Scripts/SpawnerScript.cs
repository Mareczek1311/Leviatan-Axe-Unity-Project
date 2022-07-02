using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float time = 3, timebtw;
    public Transform[] spawners;
    public GameObject enemy;
    bool isReady;

    private void Start()
    {
        timebtw = time;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            isReady = !isReady;
        }
        if (isReady)
        {
            if (timebtw <= 0)
            {
                int random = Random.RandomRange(0, spawners.Length - 1);

                Instantiate(enemy, new Vector3(spawners[random].position.x, 1.494f, spawners[random].position.z), Quaternion.identity);
                timebtw = time;
            }
            else
            {
                timebtw -= Time.deltaTime;
            }
        }
    }

}
