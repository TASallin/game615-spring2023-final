using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MegaSteve : MonoBehaviour
{
    public int hp;
    public int state; //0 = idle, 1 = spin, 2 = burn
    public Transform player;
    public Animator anim;
    public NavMeshAgent agent;
    float attackCooldown;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;
    public GameObject burnerA;
    public GameObject burnerB;
    public GameObject burnerC;
    public GameObject burnerD;
    public Vector3 knobA;
    public Vector3 knobB;
    public Vector3 knobC;
    public Vector3 knobD;
    public GameObject phase2;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0) {
            UpdateIdle();
        } else if (state == 1) {
            UpdateSpin();
        }
    }

    void UpdateIdle() {
        transform.LookAt(player.position);
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) {
            attackCooldown = 5f;
            StateChange();
        }
    }
    
    void UpdateSpin() {

    }

    void StateChange() {
        if (hp <= 0) {
            return;
        }
        if (state > 0) {
            state = 0;
            Wander();
        } else {
            state = Random.Range(1, 3);
        }
        if (state == 1) {
            StartCoroutine(ISpin());
        } else if (state == 2) {
            StartCoroutine(IBurner());
        }
    }

    IEnumerator ISpin() {
        agent.enabled = false;
        anim.SetBool("Spin", true);
        float countdown = 3f;
        while (countdown > 0) {
            countdown -= Time.deltaTime;
            transform.Translate(0, 2 * Time.deltaTime, 0);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Vector3 target = player.position;
        Vector3 origin = transform.position;
        target.y = transform.position.y;
        target = (target - transform.position) * 2;
        target.y = transform.position.y;
        Vector3 targetDir = Vector3.Normalize(target);
        countdown = 4f;
        while (countdown > 0) {
            Vector3 positionNoY = new Vector3(transform.position.x, target.y, transform.position.z);
            Vector3 newPosition = positionNoY + targetDir * Time.deltaTime * 20;
            float yValue = target.y - 12 + 12 * Mathf.Abs(0.5f - countdown / 4f);
            float xValue = Mathf.Clamp(newPosition.x, minX, maxX);
            float zValue = Mathf.Clamp(newPosition.z, minZ, maxZ);
            countdown -= Time.deltaTime;
            transform.position = new Vector3(xValue, yValue, zValue);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
        yield return new WaitForSeconds(1f);
        target = player.position;
        origin = transform.position;
        target.y = transform.position.y;
        target = (target - transform.position) * 2;
        target.y = transform.position.y;
        targetDir = Vector3.Normalize(target);
        countdown = 4f;
        while (countdown > 0) {
            Vector3 positionNoY = new Vector3(transform.position.x, target.y, transform.position.z);
            Vector3 newPosition = positionNoY + targetDir * Time.deltaTime * 25;
            float yValue = target.y - 12 + 12 * Mathf.Abs(0.5f - countdown / 4f);
            float xValue = Mathf.Clamp(newPosition.x, minX, maxX);
            float zValue = Mathf.Clamp(newPosition.z, minZ, maxZ);
            countdown -= Time.deltaTime;
            transform.position = new Vector3(xValue, yValue, zValue);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
        yield return new WaitForSeconds(1f);
        target = player.position;
        origin = transform.position;
        target.y = transform.position.y;
        target = (target - transform.position) * 2;
        target.y = transform.position.y;
        targetDir = Vector3.Normalize(target);
        countdown = 4f;
        while (countdown > 0) {
            Vector3 positionNoY = new Vector3(transform.position.x, target.y, transform.position.z);
            Vector3 newPosition = positionNoY + targetDir * Time.deltaTime * 30;
            float yValue = target.y - 12 + 12 * Mathf.Abs(0.5f - countdown / 4f);
            float xValue = Mathf.Clamp(newPosition.x, minX, maxX);
            float zValue = Mathf.Clamp(newPosition.z, minZ, maxZ);
            countdown -= Time.deltaTime;
            transform.position = new Vector3(xValue, yValue, zValue);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
        countdown = 3f;
        while (countdown > 0) {
            countdown -= Time.deltaTime;
            transform.Translate(0, - 2 * Time.deltaTime, 0);
            yield return null;
        }
        anim.SetBool("Spin", false);
        agent.enabled = true;
        StateChange();
    }

    IEnumerator IBurner () {
        int index = Random.Range(0, 4);
        Vector3 target;
        GameObject burner;
        switch (index) {
            case 0:
                target = knobA;
                burner = burnerA;
                break;
            case 1:
                target = knobB;
                burner = burnerB;
                break;
            case 2:
                target = knobC;
                burner = burnerC;
                break;
            default:
                target = knobD;
                burner = burnerD;
                break;
        }
        agent.SetDestination(target);
        while (Vector3.Distance(transform.position, target) > 1) {
            yield return null;
        }
        burner.SetActive(true);
        yield return new WaitForSeconds(5f);
        burner.SetActive(false);
        StateChange();
    }

    void Wander() {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        agent.SetDestination(new Vector3(x, transform.position.y, z));
    }
    
    public void Damage() {
        hp -= 1;
        if (hp <= 0) {
            StopCoroutine(ISpin());
            StopCoroutine(IBurner());
            burnerA.SetActive(false);
            burnerB.SetActive(false);
            burnerC.SetActive(false);
            burnerD.SetActive(false);
            StartCoroutine(Die());
        }
    }

    IEnumerator Die() {
        anim.SetTrigger("Dying");
        agent.enabled = false;
        while (transform.position.z < maxZ) {
            transform.Translate(0, 0, 40 * Time.deltaTime, Space.World);
            yield return null;
        }
        anim.SetTrigger("Shaking");
        transform.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(4f);
        anim.SetTrigger("Falling");
        yield return new WaitForSeconds(3f);
        phase2.SetActive(true);
        Destroy(gameObject);
    }
}
