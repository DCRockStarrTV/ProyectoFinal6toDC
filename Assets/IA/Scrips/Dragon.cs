using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BasicAgent
{
    [SerializeField] Mood agentState;
    Animator animator;
    [SerializeField] Rigidbody rb;
    string AnimationName;
    bool enemyInTerrytory = false;

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
        Mood TmpState;
        if (target == null)
        {
            TmpState = Mood.None;
        }
        else if (enemyInTerrytory)
        {
            TmpState = Mood.Pursuit;
            if (Vector3.Distance(transform.position, target.position) < stopThreshold)
            {
                TmpState = Mood.Kill;
            }
        }
        else
        {
            TmpState = Mood.Return;
        }
        changeAgentState(TmpState);
        movementManager();
    }

    void changeAgentState(Mood t_newState)
    {
        if (agentState == t_newState)
        {
            return;
        }
        agentState = t_newState;
    }

    void movementManager()
    {
        switch (agentState)
        {
            case Mood.None:
                idle();
                break;
            case Mood.Pursuit:
                seek();
                break;
            case Mood.Kill:
                attacking();
                break;
            case Mood.Return:
                returning();
                break;
        }
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

    private void attacking()
    {
        if (!AnimationName.Equals("attack"))
        {
            animator.Play("Attack1", 0);
            AnimationName = "attack";
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

    private void returning()
    {
        if (!AnimationName.Equals("walk"))
        {
            animator.Play("Walk", 0);
            AnimationName = "walk";
        }
        rb.velocity = SteeringBehaviours.seek(this, target.position);
        if (Vector3.Distance(transform.position, target.position) <= slowingRadius)
        {
            target = null;
        }
    }

    public void isEnemyInside()
    {
        enemyInTerrytory = !enemyInTerrytory;
    }

    public void setTarget(Transform t_target)
    {
        target = t_target;
    }

    private enum Mood
    {
        None,
        Pursuit,
        Kill,
        Return
    }
}