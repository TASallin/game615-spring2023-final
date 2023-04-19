using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    float countdown;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        countdown = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        countdown -= Time.deltaTime;
        if (countdown <= 0 || Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("HUB AREA");
        }
    }
}
