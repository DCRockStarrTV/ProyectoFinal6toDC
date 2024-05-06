using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviours : MonoBehaviour
{
    // Clase para implementar el comportamiento de búsqueda
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

        // Método para actualizar el comportamiento de búsqueda
        public void Update()
        {
            // Calcula la dirección hacia el objetivo
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

        // Método para actualizar el comportamiento de huida
        public void Update()
        {
            // Calcula la dirección lejos del objetivo
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

        // Método para actualizar el comportamiento de persecución
        public void Update()
        {
            // Calcula la dirección anticipando la posición futura del objetivo
            Vector3 directionToTarget = targetTransform.position - agentTransform.position;
            float targetSpeed = targetRigidbody.velocity.magnitude;
            float predictionTime = directionToTarget.magnitude / (speed + targetSpeed);
            Vector3 predictedTargetPosition = targetTransform.position + targetRigidbody.velocity * predictionTime;
            Vector3 direction = (predictedTargetPosition - agentTransform.position).normalized;

            // Calcula el movimiento del agente hacia la posición anticipada del objetivo
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

        // Método para actualizar el comportamiento de evasión
        public void Update()
        {
            // Calcula la dirección anticipando la posición futura del objetivo
            Vector3 directionToTarget = targetTransform.position - agentTransform.position;
            float targetSpeed = targetRigidbody.velocity.magnitude;
            float predictionTime = directionToTarget.magnitude / (speed + targetSpeed);
            Vector3 predictedTargetPosition = targetTransform.position + targetRigidbody.velocity * predictionTime;
            Vector3 direction = (agentTransform.position - predictedTargetPosition).normalized;

            // Calcula el movimiento del agente lejos de la posición anticipada del objetivo
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);
        }
    }

    public class Wander
    {
        private Transform agentTransform; // Transform del agente
        private Transform wanderRadiusTransform; // Transform de la esfera de radio de deambulación
        private float circleDistance; // Distancia del círculo
        private float circleRadius; // Radio del círculo
        private float angleChange; // Cambio de ángulo máximo
        private float speed; // Velocidad del agente

        // Constructor que toma el transform del agente, el transform de la esfera de radio de deambulación,
        // la distancia y el radio del círculo, el cambio de ángulo máximo y la velocidad
        public Wander(Transform agentTransform, Transform wanderRadiusTransform, float circleDistance, float circleRadius, float angleChange, float speed)
        {
            this.agentTransform = agentTransform;
            this.wanderRadiusTransform = wanderRadiusTransform;
            this.circleDistance = circleDistance;
            this.circleRadius = circleRadius;
            this.angleChange = angleChange;
            this.speed = speed;
        }

        // Método para actualizar el comportamiento de deambulación
        public void Update()
        {
            // Obtiene la posición de la esfera de radio de deambulación
            Vector3 wanderCenter = wanderRadiusTransform.position;

            // Calcula la dirección hacia el punto de deambulación
            Vector3 direction = (wanderCenter + Random.insideUnitSphere * circleDistance - agentTransform.position).normalized;

            // Calcula el movimiento del agente hacia el punto de deambulación
            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);

            // Limita la posición del agente dentro del radio de deambulación
            Vector3 wanderSphereCenter = new Vector3(wanderCenter.x, agentTransform.position.y, wanderCenter.z);
            if (Vector3.Distance(agentTransform.position, wanderSphereCenter) > circleDistance)
            {
                agentTransform.position = wanderSphereCenter + (agentTransform.position - wanderSphereCenter).normalized * circleDistance;
            }
        }
    }
}

