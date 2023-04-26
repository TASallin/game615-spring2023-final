using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
            CloseMenu();
        }
    }

    public void SetText(string t) {
        text.text = t;
    }

    public void CloseMenu() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
