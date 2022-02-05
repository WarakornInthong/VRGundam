using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{
    public float speed;
    public LayerMask layer;
    public AimCursor aimCursor;
    public GameObject WeaponArm;
    public GameObject owo;
    public GameObject planeAim;
    public Vector3 offset;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(speed >= 10){
            speed = 10;
        }
        else if(speed < 0){
            speed = 0;
        }
    }

    void LateUpdate() {
        Aiming();
    }
    private void Aiming(){

        // Not Complete
        
        // Vector3 aimVector = aimCursor.obj.position - WeaponArm.transform.position;
        MechGun gun = WeaponArm.transform.GetChild(0).GetChild(0).GetChild(5).GetComponent<MechGun>();
        Vector3 aimVector = owo.transform.position - gun.pivot.position;
        // WeaponArm.transform.up = aimVector;
        WeaponArm.transform.localEulerAngles = WeaponArm.transform.localEulerAngles + offset;
        // Quaternion rotation = Quaternion.LookRotation(aimVector);
        // WeaponArm.transform.rotation = rotation;
        // Vector3 vectorOnPlane = Vector3.ProjectOnPlane(aimVector, planeAim.transform.forward);
        // Debug.Log(vectorOnPlane);
        // float angleI = Vector3.Angle(vectorOnPlane, planeAim.transform.up);
        // float angleJ = Vector3.Angle(vectorOnPlane, planeAim.transform.right);
        // Debug.Log("i " + angleI + " j " + angleJ);
        // WeaponArm.transform.localEulerAngles =  new Vector3(Vector3.Angle(aimVector, Vector3.right), Vector3.Angle(aimVector, Vector3.up), Vector3.Angle(aimVector, Vector3.forward)) + offset;
        
        // WeaponArm.transform.up = aimCursor.obj.position - WeaponArm.transform.position;
    }
}
