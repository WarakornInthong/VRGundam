using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialJagan : MonoBehaviour
{
    // public Transform jeganTranform;
    public bool isDeploy;
    public float acceleration;
    public float speed = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDeploy){
            speed += acceleration * Time.deltaTime;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
