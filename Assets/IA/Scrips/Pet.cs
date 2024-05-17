using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : BasicAgent
{
    [SerializeField] Mood agentState;
    Animator animator;
    [SerializeField] Rigidbody rb;
    string AnimationName;
    bool feeded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agentState = Mood.None;
        AnimationName = "";
    }

    void Update()
    {
        MainDecision();//---
    }

    void MainDecision()
    {
        Mood newState;
        if (!feeded)
        {
            newState = Mood.None;
        }
        else
        {
            newState = Mood.Pursuit;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold)
            {
                newState = Mood.None;
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
            case Mood.Pursuit:
                seek();
                break;
        }
    }

    public void feed(Transform t_target)
    {
        target = t_target;
        feeded = true;
    }

    private void seek()
    {
        if (!AnimationName.Equals("walk"))
        {
            animator.Play("Walk", 0);
            AnimationName = "walk";
        }
        maxVel *= 2;
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        rb.velocity = SteeringBehaviours.arrival(this, target.position, slowingRadius, stopThreshold);
        maxVel /= 2;
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

    private enum Mood
    {
        None,
        Pursuit
    }
}
