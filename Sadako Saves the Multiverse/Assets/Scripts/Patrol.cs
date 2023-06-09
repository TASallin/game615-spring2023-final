using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public List<Vector3> patrolPositions;
    public List<int> patrolDelays;
    public List<Vector3> patrolBearings;
    public NavMeshAgent agent;
    public float moveSpeed;
    int index;
    float countdown;
    public float turnSpeed;
    float timeout;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        countdown = 0;
        agent.speed = moveSpeed;
        timeout = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0) {
            Quaternion newRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(patrolBearings[index]), Time.deltaTime * turnSpeed);
            transform.localRotation = newRotation;
            countdown -= Time.deltaTime;
            if (countdown <= 0) {
                NextPath();
            }
        } else {
            timeout -= Time.deltaTime;
            Vector3 augmentedPosition = new Vector3(transform.position.x, patrolPositions[index].y, transform.position.z);
            if (Vector3.Distance(augmentedPosition, patrolPositions[index]) < 0.2f || timeout <= 0) {
                countdown = patrolDelays[index];
            }
        }
    }

    void NextPath() {
        index = (index + 1) % patrolPositions.Count;
        agent.SetDestination(patrolPositions[index]);
        timeout = 20;
    }

    public void Reawaken() {
        float shortestDistance = 9000;
        for (int i = 0; i < patrolPositions.Count; i++) {
            float currentDistance = Vector3.Distance(transform.position, patrolPositions[i]);
            if (currentDistance < shortestDistance) {
                index = i;
                shortestDistance = currentDistance;
            }
        }
        countdown = 0;
        agent.SetDestination(patrolPositions[index]);
        agent.speed = moveSpeed;
    }
}
