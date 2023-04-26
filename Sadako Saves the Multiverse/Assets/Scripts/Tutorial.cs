using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public UpgradeUI popup;
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.game == null || !GameData.game.sawTutorial) {
            GameData.game.sawTutorial = true;
            popup.gameObject.SetActive(true);
            popup.SetText(description);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
