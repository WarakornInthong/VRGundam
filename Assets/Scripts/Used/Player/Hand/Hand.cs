using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ควบคุมการทำงานของมือทั้งหมด
[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;
    private SkinnedMeshRenderer mesh;
    private Animator animator;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;

    // ตั้งค่าการทำงานเริ่มต้นของมือ
    void Start()
    {
        // Animation
        animator = GetComponent<Animator>();

        // เเสดงผล
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        AnimateHand();
    }

    // รับค่า Grip
    public void SetGrip(float v)
    {
        gripTarget = v;
    }

    // รับค่า Trigger
    public void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void AnimateHand(){

        // กดปุ่ม Grip 
        if(gripTarget != gripCurrent){
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }

        // กดปุ่ม Trigger 
        if(triggerTarget != triggerCurrent){
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    // ควบคุมการเเสดงมือ
    public void ToggleVisibility(){
        mesh.enabled = !mesh.enabled;
    }
}
