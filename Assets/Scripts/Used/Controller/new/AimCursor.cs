using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCursor : MonoBehaviour
{
    // public GameObject cursor;
    public Vector3 posCursor;
    public Transform centerOfMonitor;
    // public GameObject target;
    public Transform obj;
    public float distance;
    // private LineRenderer line;
    public LayerMask layer;
    // public Vector3 aimDirection;
    private Vector3 aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        // line = GetComponent<LineRenderer>();
    }


    public void MapCursor(bool isActive){
        if(isActive){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.right, out hit, distance, layer)){
                // cursor.SetActive(true);
                // line.enabled = true;

                // line.SetPosition(0, transform.position);
                // line.SetPosition(1, hit.point); 
                // cursor.transform.position = hit.point;
                posCursor = hit.point - centerOfMonitor.position;
                // Debug.Log(posCursor);
                aimDirection = (hit.point - transform.position).normalized;
                obj.localPosition = new Vector3(-posCursor.z/2, posCursor.y, -0.01f);
            }
            else{
                // cursor.SetActive(false);
                // line.enabled = false;
            }
        }
        else{
            // cursor.SetActive(false);
            // line.enabled = false;
        }
    }

    public Vector3 GetAimDirection(){
        return transform.localEulerAngles;
    }

}
