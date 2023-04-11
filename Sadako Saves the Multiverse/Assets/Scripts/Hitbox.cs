using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int power;
    public bool destroyOnHit;
    public float lifetime;
    public float iframes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0) {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0) {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag.Equals("Player")) {
            Hurtbox box = other.gameObject.GetComponent<Hurtbox>();
            if (box.iframes <= 0) {
                box.Damage(power);
                box.iframes = iframes;
            }
            
        }

        if (destroyOnHit) {
            Destroy(gameObject);
        }
    }
}
