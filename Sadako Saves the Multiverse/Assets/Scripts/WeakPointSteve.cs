using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointSteve : MonoBehaviour
{
    public MegaSteve boss;
    public UltraSteve otherBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        if (boss) {
            boss.Damage();
        } else {
            otherBoss.Damage();
        }
    }
}
