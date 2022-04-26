using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedDoor : MonoBehaviour
{
    public Material defaultMaterial;
    public Material dissolveMaterial;
    public GameObject doorTarget;
    public float speed;
    public float targetAngle;
    [Range(1,2)]
    public int mode;
    private bool isClear =false;

    // Update is called once per frame
    void Update()
    {
        
        if(isClear){
            // dissolve
            if(mode == 1){
                float cutoffHeight = dissolveMaterial.GetFloat("_CutoffHeight");
                if(cutoffHeight >= -1){
                    dissolveMaterial.SetFloat("_CutoffHeight", cutoffHeight - Time.deltaTime * speed);
                }
                else{
                    doorTarget.SetActive(false);
                    doorTarget.GetComponent<Renderer>().material = defaultMaterial;
                    dissolveMaterial.SetFloat("_CutoffHeight", 4.5f);
                    isClear = false;
                }
            }
            // Angle
            else if(mode == 2){
                if(targetAngle < 0){
                    if(Mathf.Abs((doorTarget.transform.eulerAngles.y) - (360 + targetAngle)) > 0.1f)
                        doorTarget.transform.Rotate(Vector3.up * -1 * speed * Time.deltaTime);
                    else
                        isClear = false;
                }
                else{
                    if(Mathf.Abs(doorTarget.transform.eulerAngles.y - targetAngle) > 0.1f)
                        doorTarget.transform.Rotate(Vector3.up * speed * Time.deltaTime);
                    else
                        isClear = false;
                }
                    
            }
        }
        
    }

    public void OpededDoor(){
        isClear = true;
        // Debug.Log("ooo");
        if(mode == 1)
            doorTarget.GetComponent<Renderer>().material = dissolveMaterial;
    }


}
