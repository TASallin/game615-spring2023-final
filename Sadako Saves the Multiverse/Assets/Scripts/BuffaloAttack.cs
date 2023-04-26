using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffaloAttack : AttackAI
{
    float cooldown;
    bool mobile;
    public Rigidbody rb;
    float timeout;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        timeout = 0;
        mobile = true;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        timeout -= Time.deltaTime;
        if (mobile) {
            if (eyesOnPlayer) {
                agent.SetDestination(lastKnownPosition);
                timeout = 15f;
                if (cooldown <= 0 && Vector3.Distance(transform.position, lastKnownPosition) > 4f) {
                    StartAttack();
                }
            } else {
                if (agent.remainingDistance < 0.5f || timeout < 0) {
                    GiveUp();
                }
            }
        } else {
            rb.velocity = transform.forward * 14;
            if (cooldown <= 0) {
                EndAttack();
            }
        }
    }

    public override void LoseSight() {
        if (mobile) {
            agent.SetDestination(lastKnownPosition);
        }  
    }

    void StartAttack() {
        anim.SetBool("Attack", true);
        mobile = false;
        agent.speed = 0;
        agent.enabled = false;
        cooldown = 5f;
        rb.velocity = transform.forward * 11;
    }

    void OnCollisionEnter(Collision col) {
        EndAttack();
    }

    public void EndAttack() {
        anim.SetBool("Attack", false);
        mobile = true;
        agent.enabled = true;
        agent.SetDestination(lastKnownPosition);
        agent.speed = moveSpeed;
        cooldown = 0.5f;
        rb.velocity = new Vector3(0, 0, 0);
        transform.Translate(transform.forward * -1 * 0.5f);
        transform.LookAt(lastKnownPosition);
    }
}
