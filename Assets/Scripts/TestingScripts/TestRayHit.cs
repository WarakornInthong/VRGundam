using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestRayHit : MonoBehaviour
{
    XRRayInteractor XRRay;
    public GameObject AimedPic;
    void Start()
    {
        XRRay = GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(XRRay.TryGetCurrent3DRaycastHit(out RaycastHit rayhit)){
            AimedPic.SetActive(true);
            AimedPic.transform.position = rayhit.point;
            AimedPic.transform.up = rayhit.normal;
        }
        else{
            AimedPic.SetActive(false);
        }
    }

    // public void TestRayCast(){
    //     if(XRRay.TryGetCurrent3DRaycastHit(out RaycastHit rayhit)){
    //         AimedPic.transform.position = rayhit.point;
    //         AimedPic.transform.up = rayhit.normal;
    //     }
    // }
}
