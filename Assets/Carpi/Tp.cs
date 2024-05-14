using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public Transform Tpplace;
    //public Transform Tp2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = Tpplace.position;
            other.gameObject.transform.Rotate(0f, 180f, 0f);
        }

    }
}