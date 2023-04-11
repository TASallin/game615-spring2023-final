using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    public float turnSpeed;
    public float jumpStrength;
    public float hoverCostMultiplier;
    float verticalVelocity;
    const float GRAVITY = 9.8f;
    float groundTimer;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        verticalVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        bool hovering = Input.GetKey(KeyCode.LeftShift);

        verticalVelocity -= GRAVITY * Time.deltaTime;

        if (hovering && GameData.game.player.mana > 0 && verticalVelocity < 0) {
            verticalVelocity = 0;
            gm.SpendMana(Time.deltaTime * hoverCostMultiplier);
        }

        transform.Rotate(new Vector3(0, horiz * turnSpeed * Time.deltaTime, 0));

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (groundTimer > 0) {
                verticalVelocity = jumpStrength;
                groundTimer = 0;
            }
        }

        Vector3 movementVector = transform.forward * vert * moveSpeed * Time.deltaTime;
        movementVector += new Vector3(0, verticalVelocity * Time.deltaTime, 0);
        cc.Move(movementVector);

        bool grounded = cc.isGrounded;

        if (grounded && verticalVelocity < 0) {
            verticalVelocity = 0;
        }

        if (groundTimer > 0) {
            groundTimer -= Time.deltaTime;
        }

        if (grounded) {
            groundTimer = 0.2f;
        }
    }
}
