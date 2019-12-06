using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    public float turnSpeed=100,speed=5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * Input.GetAxis("Horizontal") * speed
                            ,0
                            ,Time.deltaTime * Input.GetAxis("Vertical") * speed);

        transform.Rotate(Time.deltaTime * Input.GetAxis("RVertical") * turnSpeed
                        ,Time.deltaTime * Input.GetAxis("RHorizontal") * turnSpeed
                        ,Time.deltaTime * (Input.GetAxis("LT")-Input.GetAxis("RT")) * turnSpeed);

    }
}
