using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labelLight1 : MonoBehaviour
{
    public GameObject lever;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lever.GetComponent<leverScript>().selected1 == true)
        {

        }
    }
}
