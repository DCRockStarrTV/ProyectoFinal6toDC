using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : BasicAgent
{
    [SerializeField] float eyesPerRad, earsPerRad;
    [SerializeField] Transform eyesPer, earsPer;
    [SerializeField] Mood agentState;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider[] per_eyes, per_ears;
    Animator animator;
    string AnimationName;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = Mood.None;
        AnimationName = "";
    }

    void Update()
    {
        MainPerception();//---
        MainDecision();//---
    }

    void FixedUpdate()
    {
        per_eyes = Physics.OverlapSphere(eyesPer.position, eyesPerRad);
        per_ears = Physics.OverlapSphere(earsPer.position, earsPerRad);
    }

    void MainPerception()
    {
        if (per_eyes != null)
        {
            foreach (Collider tmp in per_eyes)
            {
                if (tmp.CompareTag("Player"))
                {
                    target = tmp.transform;
                }
            }
        }
        if (per_ears != null)
        {
            foreach (Collider tmp in per_ears)
            {
                if (tmp.CompareTag("Player"))
                {
                    target = tmp.transform;
                }
            }
        }
    }

    void MainDecision()
    {
        Mood newState;
        if (target == null)
        {
            newState = Mood.None;
        }
        else
        {
            newState = Mood.Escape;
            if (Vector3.Distance(transform.position, target.position) > stopThreshold)
            {
                target = null;
            }
        }
        changeAgentState(newState);
        mainMovement();
    }

    void changeAgentState(Mood t_newState)
    {
        if (agentState == t_newState)
        {
            return;
        }
        agentState = t_newState;
    }

    void mainMovement()
    {
        switch (agentState)
        {
            case Mood.None:
                idle();
                break;
            case Mood.Wander:
                wander();
                break;
            case Mood.Escape:
                flee();
                break;
        }
    }

    private void idle()
    {
        if (!AnimationName.Equals("idle"))
        {
            animator.Play("Idle", 0);
            AnimationName = "idle";
        }
        rb.velocity = Vector3.zero;
    }

    private void wander()
    {
        if (!AnimationName.Equals("walk"))
        {
            animator.Play("Walk", 0);
            AnimationName = "walk";
        }
        if ((wanderNextPosition == null) ||
            (Vector3.Distance(transform.position, wanderNextPosition.Value) < 0.5f))
        {
            wanderNextPosition = SteeringBehaviours.wanderNextPos(this);
        }
        rb.velocity = SteeringBehaviours.seek(this, wanderNextPosition.Value);
    }

    private void flee()
    {
        if (!AnimationName.Equals("Run"))
        {
            animator.Play("Run", 0);
            AnimationName = "Run";
        }
        if (target == null)
        {
            return;
        }
        rb.velocity = SteeringBehaviours.flee(this, target.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyesPer.position, eyesPerRad);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(earsPer.position, earsPerRad);
    }
    private enum Mood
    {
        None,
        Escape,
        Wander
    }
}
