using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestNewAimCursor : MonoBehaviour
{
    public Transform AimedPic;
    public LayerMask monitorLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RayHitMonitor();
    }

    private void RayHitMonitor(){
        //Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask);
        if(Physics.Raycast(transform.position, transform.right, out RaycastHit hit, 20, monitorLayer))
            AimedPic.position = hit.point;
            AimedPic.up = hit.normal;
    }


}
