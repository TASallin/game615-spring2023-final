using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public GameManager gm;
    public float iframes;
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
        gm.LoseHP(amount);
    }
}
