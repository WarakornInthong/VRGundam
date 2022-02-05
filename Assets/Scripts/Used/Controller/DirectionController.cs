using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectionController : MonoBehaviour
{
    private DeviceBasedSnapTurnProvider snapTurn;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 defaultForward;
    private Rigidbody rig;
    [SerializeField]
    private ForceMode forceMode;
    public float speed;
    public float rSpeed;
    public AimCursor aimCursor;
    internal bool isUsed = false;
    public GameObject player;
    public GameObject target;
    public MechGun mechGun;
    public void UsedLever(bool isGrip){
        isUsed = isGrip;
    }
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
            
            float direct = transform.position.z -  defaultPosition.z;
            // Debug.Log(direct);
            if(Mathf.Abs(direct) > 0.1f){
                target.transform.Rotate(Vector3.up, Time.deltaTime * -direct * rSpeed);
                // Debug.Log(direct);
            }
            // Debug.Log(jdirect);
            if(aimCursor != null)
                aimCursor.MapCursor(isUsed);

            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            device.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);
            if(trigger)
                mechGun.fire();
        }
        else{
            snapTurn.enabled = true;
            // target.transform.up = Vector3.up;

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
