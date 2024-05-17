using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory: MonoBehaviour
{

    Dragon dragon;

    private void Start()
    {
        dragon = GameObject.Find("spider").GetComponent<Dragon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dragon.isEnemyInside();
            dragon.setTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dragon.isEnemyInside();
            dragon.setTarget(gameObject.transform);
        }
    }
}
