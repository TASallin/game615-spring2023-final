using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{

    public List<MonoBehaviour> flags;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        foreach (MonoBehaviour b in flags) {
            b.enabled = true;
        }
    }
}
