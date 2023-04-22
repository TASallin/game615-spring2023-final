using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public Transform model;
    public int id;
    const float TURNSPEED = 80f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newY = (model.eulerAngles.y + TURNSPEED * Time.deltaTime) % 360;
        model.localRotation = Quaternion.Euler(new Vector3(model.eulerAngles.x, newY, model.eulerAngles.z));
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            Collect(id);
            other.gameObject.GetComponent<PlayerController>().PingData();
            Destroy(gameObject);
        }
    }

    void Collect(int id) {
        switch (id) {
            case 0:
                GameData.game.hoverUnlock = true;
                return;
            case 4:
                GameData.game.weaponPieceA = true;
                return;
            default:
                return;
        }
    }
}
