using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public Transform bone;
    public int iterations = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position;
        for(int i = 0; i < iterations ; i++)
            AimAtTarget(bone, targetPosition);
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition){
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        bone.rotation = aimTowards * bone.rotation;
    }
}
