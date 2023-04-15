using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseAttack : AttackAI
{
    float cooldown;
    bool mobile;
    public Hitbox hitbox;
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
                if (cooldown <= 0 && Vector3.Distance(transform.position, lastKnownPosition) < 3f) {
                    StartAttack();
                }
            } else {
                if (agent.remainingDistance < 0.5f || timeout < 0) {
                    GiveUp();
                }
            }
        }
    }

    public override void LoseSight() {
      agent.SetDestination(lastKnownPosition);
    }

    void StartAttack() {
        anim.SetTrigger("Attack");
        cooldown = 8f;
        mobile = false;
        agent.speed = 0;
        rb.AddForce(transform.forward * 150);
    }

    public void Swing() {
        hitbox.enabled = true;
        rb.AddForce(transform.forward * 500);
    }

    public void EndSwing() {
        hitbox.enabled = false;
    }

    public void EndAttack() {
        mobile = true;
        agent.speed = moveSpeed;
        rb.velocity = new Vector3(0, 0, 0);
    }
}
