using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appendage : MonoBehaviour
{
    float lifetime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0) {
            transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
            lifetime -= Time.deltaTime;
        }
    }
}
