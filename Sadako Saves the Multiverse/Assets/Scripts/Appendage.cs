using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appendage : MonoBehaviour
{
    float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 30 * Time.deltaTime, 0, Space.World);

    }
}
