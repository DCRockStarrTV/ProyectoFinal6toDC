using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviours : MonoBehaviour
{
    // Clase para implementar el comportamiento de b�squeda
    public class Seek //----------------------------------------------------------------------------SEEK---------------------------------------------
    {
        private Transform agentTransform; // Transform del agente
        private Transform targetTransform; // Transform del objetivo
        private float speed; // Velocidad del agente

        // Constructor que toma el transform del agente, el transform del objetivo y la velocidad
        public Seek(Transform agentTransform, Transform targetTransform, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.speed = speed;
        }

        // M�todo para actualizar el comportamiento de b�squeda
        public void Update()
        {
            // Calcula la direcci�n hacia el objetivo
            Vector3 direction = (targetTransform.position - agentTransform.position).normalized;

            // Calcula el movimiento del agente hacia el objetivo
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);
        }
    }

    public class Flee //----------------------------------------------------------------------------FLEE---------------------------------------------
    {
        private Transform agentTransform; // Transform del agente
        private Transform targetTransform; // Transform del objetivo
        private float speed; // Velocidad del agente

        // Constructor que toma el transform del agente, el transform del objetivo y la velocidad
        public Flee(Transform agentTransform, Transform targetTransform, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.speed = speed;
        }

        // M�todo para actualizar el comportamiento de huida
        public void Update()
        {
            // Calcula la direcci�n lejos del objetivo
            Vector3 direction = (agentTransform.position - targetTransform.position).normalized;

            // Calcula el movimiento del agente lejos del objetivo
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);
        }
    }
    public class Pursuit //-------------------------------------------------------------------------PURSUIT-------------------------------------------
    {
        private Transform agentTransform; // Transform del agente
        private Transform targetTransform; // Transform del objetivo
        private Rigidbody targetRigidbody; // Rigidbody del objetivo
        private float speed; // Velocidad del agente

        // Constructor que toma el transform del agente, el transform del objetivo y la velocidad
        public Pursuit(Transform agentTransform, Transform targetTransform, Rigidbody targetRigidbody, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.targetRigidbody = targetRigidbody;
            this.speed = speed;
        }

        // M�todo para actualizar el comportamiento de persecuci�n
        public void Update()
        {
            // Calcula la direcci�n anticipando la posici�n futura del objetivo
            Vector3 directionToTarget = targetTransform.position - agentTransform.position;
            float targetSpeed = targetRigidbody.velocity.magnitude;
            float predictionTime = directionToTarget.magnitude / (speed + targetSpeed);
            Vector3 predictedTargetPosition = targetTransform.position + targetRigidbody.velocity * predictionTime;
            Vector3 direction = (predictedTargetPosition - agentTransform.position).normalized;

            // Calcula el movimiento del agente hacia la posici�n anticipada del objetivo
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);
        }
    }

    public class Evade //-------------------------------------------------------------------------EVADE-------------------------------------------
    {
        private Transform agentTransform; // Transform del agente
        private Transform targetTransform; // Transform del objetivo
        private Rigidbody targetRigidbody; // Rigidbody del objetivo
        private float speed; // Velocidad del agente

        // Constructor que toma el transform del agente, el transform del objetivo y la velocidad
        public Evade(Transform agentTransform, Transform targetTransform, Rigidbody targetRigidbody, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.targetRigidbody = targetRigidbody;
            this.speed = speed;
        }

        // M�todo para actualizar el comportamiento de evasi�n
        public void Update()
        {
            // Calcula la direcci�n anticipando la posici�n futura del objetivo
            Vector3 directionToTarget = targetTransform.position - agentTransform.position;
            float targetSpeed = targetRigidbody.velocity.magnitude;
            float predictionTime = directionToTarget.magnitude / (speed + targetSpeed);
            Vector3 predictedTargetPosition = targetTransform.position + targetRigidbody.velocity * predictionTime;
            Vector3 direction = (agentTransform.position - predictedTargetPosition).normalized;

            // Calcula el movimiento del agente lejos de la posici�n anticipada del objetivo
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);
        }
    }

    public class Wander
    {
        private Transform agentTransform; // Transform del agente
        private Transform wanderRadiusTransform; // Transform de la esfera de radio de deambulaci�n
        private float circleDistance; // Distancia del c�rculo
        private float circleRadius; // Radio del c�rculo
        private float angleChange; // Cambio de �ngulo m�ximo
        private float speed; // Velocidad del agente

        // Constructor que toma el transform del agente, el transform de la esfera de radio de deambulaci�n,
        // la distancia y el radio del c�rculo, el cambio de �ngulo m�ximo y la velocidad
        public Wander(Transform agentTransform, Transform wanderRadiusTransform, float circleDistance, float circleRadius, float angleChange, float speed)
        {
            this.agentTransform = agentTransform;
            this.wanderRadiusTransform = wanderRadiusTransform;
            this.circleDistance = circleDistance;
            this.circleRadius = circleRadius;
            this.angleChange = angleChange;
            this.speed = speed;
        }

        // M�todo para actualizar el comportamiento de deambulaci�n
        public void Update()
        {
            // Obtiene la posici�n de la esfera de radio de deambulaci�n
            Vector3 wanderCenter = wanderRadiusTransform.position;

            // Calcula la direcci�n hacia el punto de deambulaci�n
            Vector3 direction = (wanderCenter + Random.insideUnitSphere * circleDistance - agentTransform.position).normalized;

            // Calcula el movimiento del agente hacia el punto de deambulaci�n
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);

            // Limita la posici�n del agente dentro del radio de deambulaci�n
            Vector3 wanderSphereCenter = new Vector3(wanderCenter.x, agentTransform.position.y, wanderCenter.z);
            if (Vector3.Distance(agentTransform.position, wanderSphereCenter) > circleDistance)
            {
                agentTransform.position = wanderSphereCenter + (agentTransform.position - wanderSphereCenter).normalized * circleDistance;
            }
        }
    }
}

