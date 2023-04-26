using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public GameManager gm;
    public float iframes;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iframes > 0) {
            iframes -= Time.deltaTime;
        }  
    }

    public void Damage(int amount) {
        if (gameObject.tag.Equals("Player")) {
            anim.SetTrigger("Damage");
            gm.LoseHP(amount);
        } else if (GetComponent<EnemyScript>() != null) {
            anim.SetTrigger("Damage");
            EnemyScript e = GetComponent<EnemyScript>();
            e.Damage(amount);
        } else if (GetComponent<WeakPointSteve>() != null) {
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
