using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxSpeed = 5.0f; 
    public float slowedSpeed = 1.5f; 
    public int maxHealth = 5;
    public GameObject projectilePrefab;
    public GameObject damagePrefab;
    public AudioClip throwCog;
    public AudioClip takeDamage;

    int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    Rigidbody2D rigidbody2d;
    Animator animator;
    AudioSource audioSource;
    GameOverManager gameOverManager;

    Vector2 lookDirection = new Vector2(1, 0);
    bool isSlowed = false; 

    public int CurrentHealth // Public getter method for currentHealth
    {
        get { return currentHealth; }
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    void Update()
    {
        if (!isInvincible)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(horizontal, vertical).normalized;

            if (move != Vector2.zero)
            {
                lookDirection = move;
            }

            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", Mathf.Clamp(move.magnitude, 0.0f, 1.0f) * (isSlowed ? slowedSpeed : 1.0f)); 

            if (Input.GetKeyDown(KeyCode.C))
            {
                Launch();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {
                        character.DisplayDialog();
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!isInvincible)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(horizontal, vertical).normalized * (isSlowed ? slowedSpeed : 1.0f); 
            rigidbody2d.velocity = move * speed;

            if (rigidbody2d.velocity.magnitude > maxSpeed)
            {
                rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxSpeed;
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = 2.0f;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(takeDamage);
            Instantiate(damagePrefab, transform.position, Quaternion.identity);

            if (currentHealth + amount <= 0)
            {
                gameOverManager.EndGame();
            }

            if (amount > 0)
            {
                isInvincible = false;
            }
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(throwCog);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    
    public void ApplySpeedModifier(float modifier)
    {
        speed *= modifier;
        isSlowed = true; 
    }

    
    public void RemoveSpeedModifier(float modifier)
    {
        speed /= modifier;
        isSlowed = false; 
    }
}
