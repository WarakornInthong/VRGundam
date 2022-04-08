using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimCursor : MonoBehaviour
{
    // public GameObject cursor;
    public Vector3 posCursor;
    public Transform centerOfMonitor;
    // public GameObject target;
    public Transform obj;
    public GameObject aimImage;
    public Transform normalPlane;
    public Transform rayOrigin;
    public Transform realTarget;
    public float distance;
    // private LineRenderer line;
    public LayerMask layer;
    // public Vector3 aimDirection;
    private Vector3 aimDirection;
    
    private Vector3 hitPoint;

    public float time =1;
    void Start()
    {
        // line = GetComponent<LineRenderer>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distance);

        if(hitPoint != Vector3.zero){
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitPoint, 0.02f);
        }

    }

    public void MapCursor(bool isActive){
        if(isActive){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.right, out hit, distance, layer)){
                // cursor.SetActive(true);
                // line.enabled = true;
                hitPoint = hit.point;
                // line.SetPosition(0, transform.position);
                // line.SetPosition(1, hit.point); 
                // cursor.transform.position = hit.point;
                posCursor = hit.point - centerOfMonitor.position;
                // Debug.Log(posCursor);
                aimDirection = (hit.point - transform.position).normalized;
                Vector3 newObj = new Vector3(-posCursor.z/2, posCursor.y, -0.01f);
                obj.localPosition = new Vector3(-posCursor.z/2, posCursor.y, -0.01f);
                if(aimImage){
                    //Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo);\
                    if(Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit objectHit))
                    {
                        realTarget.position = objectHit.point;
                        Vector2 aimPosition = Vector3.ProjectOnPlane(objectHit.point-normalPlane.position, normalPlane.forward);
                        // Debug.Log(aimPosition);
                        aimImage.GetComponent<Transform>().localPosition = aimPosition ;
                    }
                }
                // obj.localPosition = Vector3.Lerp(newObj ,obj.localPosition,0);
                
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
