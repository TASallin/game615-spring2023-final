using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int hp;
    public Patrol patrol;
    public AttackAI attackAI;
    public float sightRange;
    public float hearingRange;
    public float fov;
    public Transform player;
    bool aggroed;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        aggroed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aggroed) {
            float angleToPlayer = Vector3.Angle(player.position - transform.position, transform.forward);
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer <= sightRange && angleToPlayer <= fov) {
                if (!Physics.Raycast(transform.position, Vector3.Normalize(player.position - transform.position), distanceToPlayer, layerMask)) {
                    aggroed = true;
                    patrol.enabled = false;
                    attackAI.enabled = true;
                    attackAI.Aggro(player.position);
                }
            }
        } else {
            float angleToPlayer = Vector3.Angle(player.position - transform.position, transform.forward);
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer <= sightRange && angleToPlayer <= fov && !Physics.Raycast(transform.position, Vector3.Normalize(player.position - transform.position), distanceToPlayer, layerMask)) {
                attackAI.eyesOnPlayer = true;
                attackAI.lastKnownPosition = player.position;
            } else {
                if (attackAI.eyesOnPlayer == true) {
                    attackAI.LoseSight();
                }
                attackAI.eyesOnPlayer = false;
            }
        }
    }

    public void DeAggro() {
        aggroed = false;
        patrol.enabled = true;
        attackAI.enabled = false;
        patrol.Reawaken();
    }

    public void Damage(int amount) {
        hp -= amount;
        if (hp <= 0) {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    
}
