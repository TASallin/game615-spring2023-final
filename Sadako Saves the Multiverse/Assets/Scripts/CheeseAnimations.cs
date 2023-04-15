using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseAnimations : MonoBehaviour
{
    public CheeseAttack c;

    public void Swing() {
        c.Swing();
    }

    public void EndSwing() {
        c.EndSwing();
    }

    public void EndAttack() {
        c.EndAttack();
    }
}
