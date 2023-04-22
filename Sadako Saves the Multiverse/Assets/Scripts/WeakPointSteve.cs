using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointSteve : MonoBehaviour
{
    public MegaSteve boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        boss.Damage();
    }
}
