using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleAttack : AttackAI {
    float cooldown;
    bool aiming;
    public GameObject pineapple;
    public GameObject pineappleInstance;
    Vector3 pineappleSpawnPoint;
    public float turnSpeed;
    public Transform modelTransform;

    // Start is called before the first frame update
    void Start() {
        cooldown = 0;
        aiming = false;
        pineappleSpawnPoint = pineappleInstance.transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        cooldown -= Time.deltaTime;
        if (aiming) {
            transform.LookAt(lastKnownPosition);
        } else {
            if (!eyesOnPlayer) {
                GiveUp();
            } else {
                if (cooldown <= 0) {
                    StartAttack();
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
        aiming = true;
        pineappleInstance.GetComponent<PineappleBomb>().LightFuse();
    }

    public void Throw() {
        pineappleInstance.transform.parent = null;
        Rigidbody rb = pineappleInstance.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        GetComponent<Collider>().isTrigger = true;
        pineappleInstance.GetComponent<Collider>().isTrigger = false;
        float distance = Vector3.Distance(transform.position, lastKnownPosition);
        float force = Mathf.Min(500, distance * 20);
        rb.AddForce(transform.forward * force + transform.up * force);
    }

    public void EndAttack() {
        aiming = false;
        GetComponent<Collider>().isTrigger = false;
        pineappleInstance = Instantiate(pineapple, pineappleSpawnPoint, Quaternion.identity, modelTransform);
        pineappleInstance.transform.localPosition = pineappleSpawnPoint;
    }
}
