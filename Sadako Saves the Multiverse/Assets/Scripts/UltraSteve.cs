using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraSteve : MonoBehaviour
{
    public int hp;
    public int state; //0 = idle, 1 = steve summon, 2 = attack, 3 = stun
    public Transform player;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;
    public float spawnHeight;
    public float attackHeight;
    public GameObject summonA;
    public GameObject summonB;
    public GameObject summonC;
    public GameObject weakPointA;
    public GameObject weakPointB;
    public GameObject weakPointC;
    float attackCooldown;
    public GameObject steveStrike;
    public GameObject steveRain;
    public GameObject arms;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 5f;
        arms.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0) {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0) {
                attackCooldown = 5f;
                StateChange();
            }
        }
    }

    void StateChange() {
        if (state == 3) {
            state = 0;
        } else if (state == 0) {
            state = Random.Range(1, 3);
            if (state == 1) {
                StartCoroutine(SteveSummon());
            } else if (state == 2) {
                StartCoroutine(AttackMode());
            }
        } else {
            state = 3;
            StartCoroutine(Stun());
        }
    }

    IEnumerator SteveSummon() {
        steveRain.SetActive(true);
        yield return new WaitForSeconds(3f);
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        GameObject steve;
        switch (Random.Range(0, 3)) {
            case 0:
                steve = Instantiate(summonA, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
            case 1:
                steve = Instantiate(summonB, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
            default:
                steve = Instantiate(summonC, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
        }
        SetupSummon(steve);
        yield return new WaitForSeconds(1f);
        x = Random.Range(minX, maxX);
        z = Random.Range(minZ, maxZ);
        switch (Random.Range(0, 3)) {
            case 0:
                steve = Instantiate(summonA, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
            case 1:
                steve = Instantiate(summonB, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
            default:
                steve = Instantiate(summonC, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
        }
        SetupSummon(steve);
        yield return new WaitForSeconds(1f);
        x = Random.Range(minX, maxX);
        z = Random.Range(minZ, maxZ);
        switch (Random.Range(0, 3)) {
            case 0:
                steve = Instantiate(summonA, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
            case 1:
                steve = Instantiate(summonB, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
            default:
                steve = Instantiate(summonC, new Vector3(x, spawnHeight, z), Quaternion.identity);
                break;
        }
        SetupSummon(steve);
        yield return new WaitForSeconds(3f);
        steveRain.SetActive(false);
        StateChange();
    }

    void SetupSummon(GameObject steve) {
        EnemyScript eScript = steve.GetComponent<EnemyScript>();
        //steve.transform.localScale = new Vector3(2, 2, 2);
        Patrol patrol = steve.GetComponent<Patrol>();
        eScript.player = player;
        eScript.fov = 180;
        eScript.sightRange *= 3;
        patrol.patrolPositions = new List<Vector3>();
        patrol.patrolPositions.Add(steve.transform.position);
        patrol.patrolDelays = new List<int>();
        patrol.patrolDelays.Add(5);
        patrol.patrolPositions = new List<Vector3>();
        patrol.patrolBearings.Add(new Vector3(0, 0, 0));
    }

    IEnumerator Stun() {
        switch (hp) {
            case 3:
                weakPointA.SetActive(true);
                break;
            case 2:
                weakPointB.SetActive(true);
                break;
            case 1:
                weakPointC.SetActive(true);
                break;
        }
        yield return null;
    }

    public void Damage() {
        hp -= 1;
        if (hp <= 0) {
            Debug.Log("Kill");
        } else {
            StateChange();
        }
    }

    IEnumerator AttackMode() {
        Vector3 target = player.position;
        target.y = transform.position.y;
        Vector3 direction = Vector3.Normalize(target - transform.position);
        direction.y = 0;
        yield return new WaitForSeconds(1f);
        for (int i = 1; i <= 5; i++) {
            Vector3 lockOn = transform.position + direction * i * 10;
            lockOn.y = attackHeight;
            Instantiate(steveStrike, lockOn, Quaternion.identity);
            yield return new WaitForSeconds(.5f);
        }
        StateChange();
    }
}
