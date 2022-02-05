using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectionJoy : Joy
{
    private DeviceBasedSnapTurnProvider snapTurn;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 defaultForward;
    private Rigidbody rig;
    [SerializeField]
    private ForceMode forceMode;
    public float speed;
    public AimCursor aimCursor;
    void Start()
    {
        snapTurn = player.GetComponent<DeviceBasedSnapTurnProvider>();
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultForward = transform.forward;
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isUsed){
            snapTurn.enabled = false;
            Vector3 jdirect = Vector3.ProjectOnPlane(joyDirection.up, Vector3.up).normalized;
            // if(jdirect.x > jdirect.z)
            target.transform.Rotate(Vector3.up, Time.deltaTime * jdirect.x * 30);
            // Debug.Log(jdirect);
            if(aimCursor != null)
                aimCursor.MapCursor(isUsed);
        }
        else{
            snapTurn.enabled = true;
            target.transform.up = Vector3.up;

            if(Vector3.Distance(defaultPosition, transform.position) > 0.1f){
                rig.AddForce((defaultPosition - transform.position).normalized * speed , forceMode);
            }
            else{
                rig.velocity = Vector3.zero;
                rig.angularVelocity = Vector3.zero;
                transform.position = defaultPosition;
                transform.forward = defaultForward;
            }

        }
    }

    public void ResetPostion(){
        transform.rotation = defaultRotation;
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
    }
}
