using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycastLine : MonoBehaviour
{
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 50);
    }
}
