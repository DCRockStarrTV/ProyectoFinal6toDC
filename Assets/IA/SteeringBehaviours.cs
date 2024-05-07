using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviours : MonoBehaviour
{
    public class Seek //----------------------------------------------------------------------------SEEK---------------------------------------------
    {
        private Transform agentTransform; // Transform del agente
        private Transform targetTransform; // Transform del objetivo
        private float speed; // Velocidad del agente

        // El constructor obvi
        public Seek(Transform agentTransform, Transform targetTransform, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.speed = speed;
        }

        
        public void Update()
        {
            // Esta wea calcula la direcci�n hacia el objetivo
            Vector3 direction = (targetTransform.position - agentTransform.position).normalized;

            // Esta wea calcula el movimiento del agente hacia el objetivo
            Vector3 movement = direction * speed * Time.deltaTime;

            // El movimiento al agente
            agentTransform.Translate(movement);
        }
    }

    public class Flee //----------------------------------------------------------------------------FLEE---------------------------------------------
    {
        private Transform agentTransform; 
        private Transform targetTransform; 
        private float speed; 

        // Constructor...
        public Flee(Transform agentTransform, Transform targetTransform, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.speed = speed;
        }

        public void Update()
        {
            Vector3 direction = (agentTransform.position - targetTransform.position).normalized;
            Vector3 movement = direction * speed * Time.deltaTime;
            agentTransform.Translate(movement);
        }
    }
    public class Pursuit //-------------------------------------------------------------------------PURSUIT------------------------------------------- 
    {
        private Transform agentTransform; 
        private Transform targetTransform; 
        private Rigidbody targetRigidbody; // Rigidbody del objetivo (determina la direccion del mi agente)
        private float speed;

        // Constructor...
        public Pursuit(Transform agentTransform, Transform targetTransform, Rigidbody targetRigidbody, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.targetRigidbody = targetRigidbody;
            this.speed = speed;
        }

        public void Update()
        {
            // Calcula la direcci�n anticipando la posici�n futura del objetivo.
            Vector3 directionToTarget = targetTransform.position - agentTransform.position;
            float targetSpeed = targetRigidbody.velocity.magnitude;
            float predictionTime = directionToTarget.magnitude / (speed + targetSpeed);
            Vector3 predictedTargetPosition = targetTransform.position + targetRigidbody.velocity * predictionTime;
            Vector3 direction = (predictedTargetPosition - agentTransform.position).normalized;

            Vector3 movement = direction * speed * Time.deltaTime;

            // Aplica el movimiento al agente
            agentTransform.Translate(movement);
        }
    }

    public class Evade //-------------------------------------------------------------------------EVADE-------------------------------------------
    {
        private Transform agentTransform; 
        private Transform targetTransform; 
        private Rigidbody targetRigidbody; 
        private float speed; 

        // Constructor...
        public Evade(Transform agentTransform, Transform targetTransform, Rigidbody targetRigidbody, float speed)
        {
            this.agentTransform = agentTransform;
            this.targetTransform = targetTransform;
            this.targetRigidbody = targetRigidbody;
            this.speed = speed;
        }

        public void Update()
        {
            Vector3 directionToTarget = targetTransform.position - agentTransform.position;
            float targetSpeed = targetRigidbody.velocity.magnitude;
            float predictionTime = directionToTarget.magnitude / (speed + targetSpeed);
            Vector3 predictedTargetPosition = targetTransform.position + targetRigidbody.velocity * predictionTime;
            Vector3 direction = (agentTransform.position - predictedTargetPosition).normalized;

            Vector3 movement = direction * speed * Time.deltaTime;

            agentTransform.Translate(movement);
        }
    }

    public class Wander //-------------------------------------------------------------------------WANDER------------------------------------------
    {
        private Transform agentTransform; 
        private Transform wanderRadiusTransform; // Transform de la esfera de radio para que patrulle
        private float circleDistance; // Distancia del c�rculo
        private float circleRadius; // Radio del c�rculo
        private float angleChange; // Cambio de �ngulo m�ximo
        private float speed; 

        // Constructor...
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

        public void Update()
        {
            // Obtiene la posici�n de la esfera de radio de deambulaci�n
            Vector3 wanderCenter = wanderRadiusTransform.position;

            // Esta wea calcula la direcci�n hacia el punto de deambulaci�n
            Vector3 direction = (wanderCenter + Random.insideUnitSphere * circleDistance - agentTransform.position).normalized;

            // Esta wea calcula el movimiento del agente hacia el punto de deambulaci�n
            Vector3 movement = direction * speed * Time.deltaTime;

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

