using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    public float turnSpeed;
    public float jumpStrength;
    public float hoverCostMultiplier;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public GameObject gun;
    float verticalVelocity;
    const float GRAVITY = 9.8f;
    float groundTimer;
    public GameManager gm;
    bool stun;
    bool canShoot;
    public Animator gunAnim;

    // Start is called before the first frame update
    void Start()
    {
        verticalVelocity = 0;
        canShoot = true;
        try {
            PingData();
        } catch (Exception e) {

        }
    }

    // Update is called once per frame
    void Update()
    {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        bool hovering = Input.GetKey(KeyCode.LeftShift);
        if (!GameData.game.hoverUnlock) {
            hovering = false;
        }

        if (stun) {
            horiz = 0;
            vert = 0;
            hovering = false;
        }

        verticalVelocity -= GRAVITY * Time.deltaTime;

        if (hovering && GameData.game.player.mana > 0 && verticalVelocity < 0) {
            verticalVelocity = 0;
            gm.SpendMana(Time.deltaTime * hoverCostMultiplier);
        }

        transform.Rotate(new Vector3(0, horiz * turnSpeed * Time.deltaTime, 0));

        if (Input.GetKeyDown(KeyCode.Space) && !stun) {
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

        if (canShoot && GameData.game.weaponPieceA && Input.GetKeyDown(KeyCode.H)) {
            StartCoroutine(Shoot());
        }
    }

    public void StunPlayer() {
        stun = true;
    }

    public void UnstunPlayer() {
        stun = false;
    }

    IEnumerator Shoot() {
        canShoot = false;
        gunAnim.SetTrigger("Fire");
        Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }

    public void PingData() {
        if (GameData.game.weaponPieceA) {
            gun.SetActive(true);
        }
    }
}
