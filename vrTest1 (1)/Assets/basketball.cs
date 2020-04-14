using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class basketball : MonoBehaviour
{
    Vector3 beginningPosition;
    public GameObject globalVars;
    // Start is called before the first frame update
    void Start()
    {
        beginningPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position!=beginningPosition)
        {
            globalVars.GetComponent<globalVars>().ballGrabbed = true;
        }
        if (globalVars.GetComponent<globalVars>().playMode!=0)
        {
            Destroy(gameObject);
        }
    }
}
