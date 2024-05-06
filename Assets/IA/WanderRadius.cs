using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderRadius : MonoBehaviour
{
    public Transform agent; // Transform del agente al que quieres aplicar el comportamiento de Wander
    public float wanderRadius = 10f; // Radio de deambulaci�n

    void Update()
    {
        // Aseg�rate de que el objeto de la esfera siga al agente
        transform.position = agent.position;

        // Actualiza el escala de la esfera para representar el radio de deambulaci�n
        transform.localScale = new Vector3(wanderRadius * 2f, wanderRadius * 2f, wanderRadius * 2f);
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja una esfera gizmo para visualizar el radio de deambulaci�n en el editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
