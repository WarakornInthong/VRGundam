using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class Lever : MonoBehaviour
{
    public GameObject hand;
    internal HingeJoint controller;
    internal bool isUsed = false;
    [SerializeField]internal XRNode inputSource;
    public GameObject player;
    internal Vector2 inputAxis;
    internal float speed;
    public GameObject target;
    

    // Start is called before the first frame update
    void Start()
    {
        // SetVisibleHand(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UsedLever(bool isGrip){
        if(isGrip){
            SkinnedMeshRenderer handMesh = hand.GetComponentInChildren<SkinnedMeshRenderer>();
            Animator animator = hand.GetComponent<Animator>();
            handMesh.enabled = true;
            animator.SetFloat("Grip", 1);
            isUsed = true;
            // Debug.Log("owo");
        }
        else{
            SkinnedMeshRenderer handMesh = hand.GetComponentInChildren<SkinnedMeshRenderer>();
            Animator animator = hand.GetComponent<Animator>();
            handMesh.enabled = false;
            animator.SetFloat("Grip", 0);
            isUsed = false;
        }
    }

    public void SetVisibleHand(bool isHover){
        SkinnedMeshRenderer handMesh = hand.GetComponentInChildren<SkinnedMeshRenderer>();
        handMesh.enabled = isHover;
        if(isHover)
            handMesh.material.color = new Color(255, 255, 255, 150);
    }

}
