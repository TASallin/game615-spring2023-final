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
        anim.SetTrigger("Damage");
        if (gameObject.tag.Equals("Player")) {
            gm.LoseHP(amount);
        } else if (GetComponent<EnemyScript>() != null) {
            EnemyScript e = GetComponent<EnemyScript>();
            e.Damage(amount);
        }
    }
}
