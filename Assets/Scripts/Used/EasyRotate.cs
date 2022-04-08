using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyRotate : MonoBehaviour
{
    public Transform target;
    [Range(0.5f,5)]
    public float distance;
    
    [Range(1,3)]
    public int mode;
    void Start()
    {
        if(mode == 0){
            mode = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 1){
            MoveCamera();
        }
    }

    void FixedUpdate() {
        if(mode == 2){
            MoveCamera();
        }
    }

    void LateUpdate()
    {
        if(mode == 3){
            MoveCamera();
        }
    }
    private void MoveCamera(){
        float angle = Vector3.Angle(transform.forward, target.forward);
        float owo = transform.forward.magnitude* Mathf.Cos(Mathf.Deg2Rad * angle);
        
        Vector3 newPos = (owo * target.forward).normalized * distance;

        transform.position = target.position + newPos ;
        transform.eulerAngles = target.eulerAngles;
    }
}
