using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI() {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.eye.transform.position,Vector3.up,Vector3.forward,360,fow.viewRadius);

        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle/2,false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle/2,false);
        Handles.DrawLine(fow.eye.position,fow.eye.transform.position + viewAngleA * fow.viewRadius);//วาดเส้นมุมด้านซ้าย
        Handles.DrawLine(fow.eye.position,fow.eye.transform.position + viewAngleB * fow.viewRadius);//วาดเส้นมุมด้านขวา

        //วาดเส้นระหว่างtarget(ตัวเรา)กับศัตรูสีน้ำเงิน
        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets){
            Handles.DrawLine(fow.eye.position, visibleTarget.position);
        }
        Handles.color = Color.blue;
        // foreach (Transform visibleTarget in fow.visibleTargets){
            Handles.DrawLine(fow.eye.position, fow.castPoint);
        // }
    }
}
