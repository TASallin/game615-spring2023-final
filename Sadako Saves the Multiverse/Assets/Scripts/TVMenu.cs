using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TVMenu : MonoBehaviour
{
    public PlayerSpawner spawner;
    public List<string> locationNames;
    public List<GameObject> locationScreens;
    public TMP_Text locationText;
    public int location;

    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateDisplay() {
        locationText.text = locationNames[location];
        for (int i = 0; i < locationScreens.Count; i++) { 
            if (i != location) {
                locationScreens[i].SetActive(false);
            } else {
                locationScreens[i].SetActive(true);
            }
        }
    }

    public void Teleport(int i) {
        Time.timeScale = 1;
        GameData.game.spawnIndex = i;
        spawner.Spawn();
        gameObject.SetActive(false);
    }
}
