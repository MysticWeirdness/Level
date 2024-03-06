using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class PlayerManager : MonoBehaviour
{
    private int health = 3;
    private int lives = 3;
    private Vector3 startingPosition;
    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        startingPosition = transform.position;
    }
    public void TakeDamage(int amt)
    {
        
        health -= amt;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if(transform.position.y < -5f)
        {
            Die();
        }
    }
    private async void Die()
    {
        lives--;
        health = 3;
        if(lives <= 0)
        {
            GameOver();
        }
        spriteRenderer.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        Time.timeScale = 0;
        gameController.DeathUI();
        await DeathTimer();
    }

    private async Task DeathTimer()
    {
        await Task.Delay(5000);
        Time.timeScale = 1;
        rb.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;
        transform.position = startingPosition;
    }
    private void GameOver()
    {
        gameController.GameOverUI();
    }
}
