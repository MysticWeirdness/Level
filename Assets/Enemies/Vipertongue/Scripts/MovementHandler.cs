using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private SnakesStateMachine stateMachine;
    [SerializeField] private CircleCollider2D attackingCollider;
    [SerializeField] private bool right = true;
    public float movDir { get; private set; } = 1;
    private float movSpeed = 150;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask playerMask;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (!right)
        {
            ToggleDirection();
        }
    }
    void Update()
    {
        CheckForCollision();
        DetectPlayer();
        rb2d.velocity = new Vector2(movDir * movSpeed * Time.deltaTime, rb2d.velocity.y);      
    }

    private void CheckForCollision()
    {
        if(Physics2D.Raycast(transform.position, Vector2.right * movDir, 1f, mask))
        {
            ToggleDirection();
        }
    }
    private void ToggleDirection()
    {
        movDir = -movDir;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private async void DetectPlayer()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right * movDir, 1f, playerMask))
        {
            await Attack();
        }
    }

    private async Task Attack()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
        attackingCollider.enabled = true;
        stateMachine.SwitchState(SnakesStateMachine.State.attacking);
        await Task.Delay(100);
        attackingCollider.enabled = false;
        Debug.Log("GOD NO PLZ HELP ME");
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}
