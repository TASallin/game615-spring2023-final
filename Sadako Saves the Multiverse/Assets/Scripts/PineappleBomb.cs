using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleBomb : MonoBehaviour
{
    public Hitbox hitbox;
    public Renderer rend;
    public float expandSpeed;
    public int fuseLength;
    public Collider col;
    public Rigidbody rb;
    bool expanding;
    // Start is called before the first frame update
    void Start()
    {
        expanding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (expanding) {
            float sizeUp = expandSpeed * Time.deltaTime;
            transform.localScale = transform.localScale + new Vector3(sizeUp, sizeUp, sizeUp);
        }
    }

    public void LightFuse() {
        StartCoroutine(BombsAway());
    }

    IEnumerator BombsAway() {
        yield return new WaitForSeconds(fuseLength);
        hitbox.enabled = true;
        rend.enabled = false;
        rb.isKinematic = true;
        col.isTrigger = true;
        expanding = true;
    }
}
