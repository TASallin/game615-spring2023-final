using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TVMenu : MonoBehaviour
{
    public PlayerSpawner spawner;
    public List<string> locationNames;
    public List<string> sceneNames;
    public List<GameObject> locationScreens;
    public TMP_Text locationText;
    public int location;
    public Button leftButton;
    public Button rightButton;
    int menuLocation;

    // Start is called before the first frame update
    void OnEnable()
    {
        menuLocation = location;
        Time.timeScale = 0;
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void CreateDisplay() {
        locationText.text = "???";
        Debug.Log(GameData.game.activatedTVs);
        for (int i = 0; i < locationScreens.Count; i++) { 
            if (i != menuLocation) {
                locationScreens[i].SetActive(false);
            } else {
                locationScreens[i].SetActive(true);
                foreach (Transform child in locationScreens[i].transform) {
                    if (GameData.game.activatedTVs[child.GetComponent<TVButton>().index]) {
                        locationText.text = locationNames[menuLocation];
                        child.gameObject.SetActive(true);
                    } else {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
        if (menuLocation + 1 < locationScreens.Count) {
            rightButton.interactable = true;
        } else {
            rightButton.interactable = false;
        }
        if (menuLocation > 0) {
            leftButton.interactable = true;
        } else {
            leftButton.interactable = false;
        }
    }

    public void Teleport(int i) {
        Time.timeScale = 1;
        GameData.game.spawnIndex = i;
        if (location == menuLocation) {
            spawner.Spawn();
            gameObject.SetActive(false);
        } else {
            SceneManager.LoadScene(sceneNames[menuLocation]);
        }
        
    }

    public void CloseMenu() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ChangeScreen(bool right) {
        if (right && menuLocation + 1 < locationScreens.Count) {
            menuLocation += 1;
            CreateDisplay();
        } else if (!right && menuLocation > 0) {
            menuLocation -= 1;
            CreateDisplay();
        }
    }
}
