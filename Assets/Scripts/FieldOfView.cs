using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius; //รอบวง
    [Range(0,360)]
    public float viewAngle; //มุม
    public Transform eye;
    public bool isSeePlayer;
    public Transform target;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Vector3 castPoint;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start(){
        isSeePlayer=false;
        StartCoroutine("FindTargetsWithDelay",.2f);
        //Debug.Log("start");
    }

    IEnumerator FindTargetsWithDelay(float delay){
        while(true){
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets(){
        visibleTargets.Clear();
        //targetInViewRadius จะ return target ทั้งหมดที่สัมผัสหรือโดนวง viewRadius
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);
        if(targetsInViewRadius.Length!=0){
            for(int i=0 ; i < targetsInViewRadius.Length ; i++){
                target = targetsInViewRadius[i].transform; //หาตำแหน่งของ target ที่เจอ
                Vector3 dirToTarget =(target.position - eye.position).normalized;
                //transform.LookAt(target);
                
                if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle/2){//เปรียบเทียบมุม
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    
                    //bool Returns true if the ray intersects with a Collider, otherwise false.
                    if(!Physics.Raycast(eye.position,dirToTarget,dstToTarget,obstacleMask)){
                        RaycastHit hit;
                        Physics.Raycast(eye.position, dirToTarget, out hit, dstToTarget, targetMask);
                        castPoint = hit.point;
                        // Debug.Log(hit.point);
                        visibleTargets.Add(target);
                        isSeePlayer=true;      
                    }
                    else{
                        isSeePlayer=false;
                    }
                }
                else{
                    isSeePlayer=false;
                }
            }
        }
        else if(isSeePlayer){
            isSeePlayer=false;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal){
        if(!angleIsGlobal){
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees*Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }
}
