using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
// public class MechBone {
//     public HumanBodyBones bone;
// }

public class TestWeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public Transform bone;
    public int iterations = 10;
    [Range(0,1)]
    public float weight = 1.0f;
    // public MechBone[] mechBones;
    // Transform[] boneTransforms;
    public float angleLimit = 90.0f;
    public float distanceLimit = 1.5f;
    void Start()
    {
        if(!targetTransform){
            Debug.Log("error");
            targetTransform = new GameObject("new Target").transform;
            targetTransform.position = transform.position + transform.forward * 10;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = GetTargetPosition();
        for(int i = 0; i < iterations ; i++){
            AimAtTarget(bone, targetPosition, weight);
        }
            
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight){
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }

    private Vector3 GetTargetPosition(){
        Vector3 targetDirection = targetTransform.position - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;

        float blendOut = 0;
        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if( targetAngle > angleLimit){
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit){
            blendOut += distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;
    }
}
