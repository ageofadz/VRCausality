using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollScript : MonoBehaviour
{
    float rollSpeed = 20f;
    public float timeLeft = 30;
    public bool rolled = false;
    public bool isVisible = false;
    public bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible == true && rolled==false)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                if (rollSpeed > 0f)
                {
                    rollSpeed = rollSpeed - .1f;
                    transform.Rotate(xAngle: 0f, yAngle: rollSpeed, zAngle: 0f);
                }
                if (rollSpeed <= 0f)
                {
                    rollSpeed = 0f;
                    rolled = true;
                    time2();
                }
            }
        }
        if (isVisible == true && rolled==true)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                print("timeLeft finished");
                isVisible = false;
                finished = true;
            }
        }
    }

    private void OnEnable()
    {
        isVisible = true;
    }
    void time2()
    {
        timeLeft = 1;
    }
}
