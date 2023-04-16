using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator anim;
    public List<Animator> spikeAnimA;
    public List<Animator> spikeAnimB;
    bool inRange;
    bool pulled;
    bool onCooldown;

    // Start is called before the first frame update
    void Start()
    {
        pulled = true;
        onCooldown = false;
        foreach (Animator a in spikeAnimA) {
            a.SetBool("On", pulled);
        }
        foreach (Animator b in spikeAnimB) {
            b.SetBool("On", !pulled);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCooldown && inRange && Input.GetKeyDown(KeyCode.I)) {
            PullLever();
        }
    }

    void PullLever() {
        pulled = !pulled;
        anim.SetBool("On", pulled);
        foreach (Animator a in spikeAnimA) {
            a.SetBool("On", pulled);
        }
        foreach (Animator b in spikeAnimB) {
            b.SetBool("On", !pulled);
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds(5);
        onCooldown = false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            inRange = false;
        }
    }
}
