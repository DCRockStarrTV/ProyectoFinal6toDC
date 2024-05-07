using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgent : MonoBehaviour
{
    public Transform target;
    public Rigidbody targetRigidbody; // Rigidbody del objetivo (necesario para el comportamiento de pursuit y evade)
    public float speed = 5f;

    public GameObject wanderRadiusObject; // Esfera que representa el radio de wander

    private SteeringBehaviours.Seek seekBehavior;
    private SteeringBehaviours.Flee fleeBehavior;
    private SteeringBehaviours.Pursuit pursuitBehavior;
    private SteeringBehaviours.Evade evadeBehavior;
    private SteeringBehaviours.Wander wanderBehavior;
    public bool isFleeing = false; // Variable para indicar si el agente est� huyendo o no
    public bool isPursuing = false; // Variable para indicar si el agente est� persiguiendo o no
    public bool isEvading = false; // Variable para indicar si el agente est� evadiendo o no
    public bool isWandering = false; // Variable para indicar si el agente est� deambulando o no

    // Inicializaci�n
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("No se ha asignado un objetivo al agente: " + gameObject.name);
            return;
        }

        if (wanderRadiusObject == null)
        {
            Debug.LogError("No se ha asignado una esfera de radio de deambulaci�n al agente: " + gameObject.name);
            return;
        }

        // Verifica si el agente debe huir, buscar, perseguir, evadir o deambular
        if (!isFleeing && !isPursuing && !isEvading && !isWandering)
        {
            // Crea una instancia del comportamiento de b�squeda (Seek)
            seekBehavior = new SteeringBehaviours.Seek(transform, target, speed);
        }
        else if (isFleeing)
        {
            // Crea una instancia del comportamiento de huida (Flee)
            fleeBehavior = new SteeringBehaviours.Flee(transform, target, speed);
        }
        else if (isPursuing)
        {
            // Crea una instancia del comportamiento de persecuci�n (Pursuit)
            pursuitBehavior = new SteeringBehaviours.Pursuit(transform, target, targetRigidbody, speed);
        }
        else if (isEvading)
        {
            // Crea una instancia del comportamiento de evasi�n (Evade)
            evadeBehavior = new SteeringBehaviours.Evade(transform, target, targetRigidbody, speed);
        }
        else if (isWandering)
        {
            // Crea una instancia del comportamiento de deambulaci�n (Wander)
            //wanderBehavior = new SteeringBehaviours.Wander(transform, wanderRadiusObject.transform, circleDistance, circleRadius, angleChange, speed);
        }
    }

    // Actualizaci�n del comportamiento
    void Update()
    {
        if (!isFleeing && !isPursuing && !isEvading && !isWandering && seekBehavior != null)
        {
            // Actualiza el comportamiento de b�squeda
            seekBehavior.Update();
        }
        else if (isFleeing && fleeBehavior != null)
        {
            // Actualiza el comportamiento de huida
            fleeBehavior.Update();
        }
        else if (isPursuing && pursuitBehavior != null)
        {
            // Actualiza el comportamiento de persecuci�n
            pursuitBehavior.Update();
        }
        else if (isEvading && evadeBehavior != null)
        {
            // Actualiza el comportamiento de evasi�n
            evadeBehavior.Update();
        }
        else if (isWandering && wanderBehavior != null)
        {
            // Actualiza el comportamiento de deambulaci�n
            wanderBehavior.Update();
        }
    }

    // M�todo para activar el comportamiento de deambulaci�n
    public void Wander()
    {
        isWandering = true;
        isFleeing = false;
        isPursuing = false;
        isEvading = false;
        seekBehavior = null; // Desactiva el comportamiento de b�squeda
        fleeBehavior = null; // Desactiva el comportamiento de huida
        pursuitBehavior = null; // Desactiva el comportamiento de persecuci�n
        evadeBehavior = null; // Desactiva el comportamiento de evasi�n
        //wanderBehavior = new SteeringBehaviours.Wander(transform, wanderRadiusObject.transform, circleDistance, circleRadius, angleChange, speed); // Activa el comportamiento de deambulaci�n
    }
}

