using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MechAimSystem : MonoBehaviour
{
    public LayerMask layer;
    public float distance;
    private LineRenderer line;
    public AimCursor aimCursor;
    // public Vector3 aim;
    public GameObject WeaponArm;
    public Transform MechBody;
    public Vector3 offset;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
    }

    // Update is called once per frame
    void LateUpdate() {
        // Debug.Log(aim.forward);
        Aiming();
        // transform.right = aimCursor.obj.position - transform.position;
    }

    private void Aiming(){
        transform.right = aimCursor.obj.position - transform.position;
        RaycastHit hit;
            if(Physics.Raycast(WeaponArm.GetComponent<MechGun>().pivot.transform.position, WeaponArm.GetComponent<MechGun>().pivot.transform.forward, out hit, distance, layer)){
            // if(Physics.Raycast(WeaponArm.transform.position, aim, out hit, distance, layer)){

                //auto aim
                // transform.right = (hit.point - transform.position).normalized;
                // Debug.Log((hit.point - transform.position).normalized);
                // transform.localEulerAngles = reciveAim + new Vector3(0,-90,0) + offset;
                
            }
            else{
                // transform.eulerAngles = aimCursor.GetAimDirection();
                // Vector3 reciveAim = new Vector3(0, -aimCursor.GetAimDirection().z , aimCursor.GetAimDirection().y);
                // transform.right = aimCursor.obj.position - transform.position;
                // transform.localEulerAngles = reciveAim + new Vector3(0,-90,0) + offset;

                // ray
                // line.SetPosition(0, WeaponArm.GetComponent<MechGun>().pivot.transform.position);
                // line.SetPosition(1, WeaponArm.GetComponent<MechGun>().pivot.transform.forward * 10 + WeaponArm.GetComponent<MechGun>().pivot.transform.position); 
                
            }
    }
}
