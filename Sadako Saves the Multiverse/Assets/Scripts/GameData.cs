using System.Collections;
using System.Collections.Generic;

public class GameData
{
    public static GameData game;
    public PlayerData player;
    public bool weaponPieceA;
    public bool weaponPieceB;
    public bool weaponPieceC;
    public bool weaponPieceD;
    public bool healthUpgradeA;
    public bool healthUpgradeB;
    public bool healthUpgradeC;
    public bool manaUpgradeA;
    public bool manaUpgradeB;
    public bool manaUpgradeC;
    public bool hoverUnlock;
    public bool invisibleUnlock;
    public int spawnIndex;
    public bool[] activatedTVs;
    public bool sawTutorial;

    public GameData() {
        player = new PlayerData();
        activatedTVs = new bool[4];
    }
    
    public int GetMaxMana() {
        int amount = 100;
        if (manaUpgradeA) {
            amount += 50;
        }
        if (manaUpgradeB) {
            amount += 50;
        }
        if (manaUpgradeC) {
            amount += 50;
        }
        return amount;
    }

    public int GetMaxHP() {
        int amount = 100;
        if (healthUpgradeA) {
            amount += 100;
        }
        if (healthUpgradeB) {
            amount += 100;
        }
        if (healthUpgradeC) {
            amount += 100;
        }
        return amount;
    }
}
