using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image manaBar;
    public Image healthBar;
    public PlayerSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.game == null) {
            GameData.game = new GameData();
        }
        
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
            GameData.game.player.hp = GameData.game.GetMaxHP();
            GameData.game.player.mana = GameData.game.GetMaxMana();
            spawner.Spawn();
        }
        healthBar.fillAmount = (float)GameData.game.player.hp / GameData.game.GetMaxHP();
    }
}
