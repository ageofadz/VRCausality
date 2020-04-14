using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidYellow : MonoBehaviour
{
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.GetComponent<spawnUFOs>().yellowSpawned==true)
        {
            Destroy(gameObject);
        }
    }
}
