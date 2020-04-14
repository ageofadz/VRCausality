using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinking : MonoBehaviour
{
    float timeLeft = 1f;
    bool hasBlinked=false;
    private Light lt;
    // Start is called before the first frame update
    void Start()
    {
        print(transform.position);
        lt=GetComponent<Light>();
    }

    //coordinates for grip button spotlight: -12.2, 1, 9.2
    //coordinates for pointer button spotlight: -12.6, 1, 9.9

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft<=0)
        {
            timeLeft = 1f;
            lt.enabled = !lt.enabled;
        }

    }

    public void moveToPointer()
    {
        Vector3 pointerCoords = new Vector3(-0.82f,14.85f,-0.06f);
        Vector3 pointerCoords2 = new Vector3(-12.6f, 1.2f, 10.0f);
        transform.position = pointerCoords2;
    }
}
