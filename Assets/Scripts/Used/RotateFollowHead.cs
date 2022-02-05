using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateFollowHead : MonoBehaviour
{
    public Transform head;
    public Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + Vector3.up * 0.2f;
        transform.eulerAngles = new Vector3(0, head.eulerAngles.y, 0);
    }
}
