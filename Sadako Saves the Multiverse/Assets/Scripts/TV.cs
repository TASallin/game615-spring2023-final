using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public int id;
    bool inRange;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.I)) {
            EnterTV();
        }
    }

    void EnterTV() {
        GameData.game.activatedTVs[id] = true;
        GameData.game.spawnIndex = id;
        menu.SetActive(true);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            inRange = false;
        }
    }
}
