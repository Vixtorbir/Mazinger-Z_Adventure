using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    AudioManager audioManager;
    Animator animator;

    private bool isShooting = false; // Flag to prevent shooting multiple times during animation

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isShooting)
        {
            StartCoroutine(ShootCoroutine());
        }
    }

    IEnumerator ShootCoroutine()
    {
        // Set isShooting flag to true
        isShooting = true;

        // Play shooting sound
        audioManager.PlaySfx(audioManager.shoot);

        // Trigger the shooting animation
        animator.SetTrigger("attack");

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        // If player is facing left, flip the bullet
        Vector3 origScale = bullet.transform.localScale;
        bullet.transform.localScale = new Vector3(
            origScale.x * (transform.localScale.x > 0 ? 1 : -1),
            origScale.y,
            origScale.z
        );

        // Wait for the animation to finish (assuming the animation is 0.5 seconds long)
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Set isShooting flag to false, allowing the player to shoot again
        isShooting = false;
    }
}
