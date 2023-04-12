using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseAttack : AttackAI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eyesOnPlayer) {
            agent.SetDestination(lastKnownPosition);
        } else {
            if (agent.remainingDistance < 0.5f) {
                GiveUp();
            }
        }
    }

    public override void LoseSight() {
        agent.SetDestination(lastKnownPosition);
    }
}
