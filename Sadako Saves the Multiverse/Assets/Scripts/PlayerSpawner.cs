using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public List<Vector3> spawnPositions;
    public List<int> spawnIDs;
    public List<Vector3> spawnBearings;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn() {
        int id = GameData.game.spawnIndex;
        int index = spawnIDs.IndexOf(id);
        Debug.Log(index);
        if (index >= 0) {
            player.GetComponent<CharacterController>().enabled = false;
            player.position = spawnPositions[index];
            player.rotation = Quaternion.Euler(spawnBearings[index]);
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
