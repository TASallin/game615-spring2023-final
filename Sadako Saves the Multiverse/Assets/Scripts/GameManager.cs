using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image manaBar;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        GameData.game = new GameData();
        GameData.game.player = new PlayerData();
        GameData.game.player.hp = 100;
        GameData.game.player.mana = 100;
        manaBar.fillAmount = GameData.game.player.mana / GameData.game.GetMaxMana();
        healthBar.fillAmount = GameData.game.player.hp / GameData.game.GetMaxHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendMana(float amount) {
        GameData.game.player.mana = Mathf.Max(GameData.game.player.mana - amount, 0);
        manaBar.fillAmount = GameData.game.player.mana / GameData.game.GetMaxMana();
    }

    public void LoseHP(int amount) {
        GameData.game.player.hp -= amount;
        if (GameData.game.player.hp <= 0) {
            GameData.game.player.hp = 0;
        }
        Debug.Log(GameData.game.player.hp);
        healthBar.fillAmount = (float)GameData.game.player.hp / GameData.game.GetMaxHP();
    }
}
