using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSteve : MonoBehaviour
{
    public int hp;
    public int state; //0 = idle, 1 = spin
    public Transform player;
    public Animator anim;
    float attackCooldown;

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
        state = 1 - state;
        if (state == 1) {
            StartCoroutine(ISpin());
        }
    }

    IEnumerator ISpin() {
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
        while (Vector3.Distance(transform.position, target) > 0.5f) {
            Vector3 positionNoY = new Vector3(transform.position.x, target.y, transform.position.z);
            Vector3 newPosition = transform.position + Vector3.Normalize(target - positionNoY) * Time.deltaTime * 10;
            float distA = Vector3.Distance(newPosition, target);
            float distB = Vector3.Distance(target, origin);
            float yValue = target.y - 3 + 6 * Mathf.Abs(0.5f - distA / distB);
            transform.position = new Vector3(newPosition.x, yValue, newPosition.z);
            yield return null;
        }
        countdown = 3f;
        while (countdown > 0) {
            countdown -= Time.deltaTime;
            transform.Translate(0, - 2 * Time.deltaTime, 0);
            yield return null;
        }
        anim.SetBool("Spin", false);
        StateChange();
    }
}
