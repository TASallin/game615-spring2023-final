using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackAI : MonoBehaviour
{
    public bool eyesOnPlayer;
    public Vector3 lastKnownPosition;
    public NavMeshAgent agent;
    public EnemyScript enemyScript;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void LoseSight() {

    }

    public void Aggro(Vector3 playerPosition) {
        eyesOnPlayer = true;
        lastKnownPosition = playerPosition;
        agent.speed = moveSpeed;
    }

    public void GiveUp() {
        enemyScript.DeAggro();
    }

}
