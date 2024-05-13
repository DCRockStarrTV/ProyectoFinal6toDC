using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.transform.position = teleportPosition.position + new Vector3(0,0,4);
            //other.transform.rotation = teleportPosition.rotation;
        }
    }
}
