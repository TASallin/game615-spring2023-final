using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleAnimation : MonoBehaviour {
    public PineappleAttack p;

    public void Throw() {
        p.Throw();
    }

    public void EndAttack() {
        p.EndAttack();
    }
}
