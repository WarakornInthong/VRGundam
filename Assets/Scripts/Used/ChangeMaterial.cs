using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material targetMaterial;
    public GameObject target;

    public void ChangeTargetMaterial(){
        target.GetComponent<Renderer>().material = targetMaterial;
    }
}
