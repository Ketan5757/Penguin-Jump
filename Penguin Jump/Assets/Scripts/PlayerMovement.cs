using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Collider2D penguinCollider;
    private float playerY;

    private Rigidbody2D rigidBody;
    private BoxCollider2D coll;
    private Animator anim;
    private float dirX = 0f;
    public static float topScore = 0.0f;
    public static float finalScore = 0.0f;
    public Text scoreText;
    private static bool fishCollected = false;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState { Idle, Running, Jumping, Falling};

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    { 
        rigidBody = GetComponent<Rigidbody2D>(); //TODO: please use tryGetComponent to prevent eventual conflicts when component not on GameObject!
        coll = GetComponent<BoxCollider2D>();
        playerY = transform.position.y;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(dirX * moveSpeed,rigidBody.velocity.y);

        var localScale = transform.localScale;
        var transformLocalScale = localScale;

        localScale = dirX switch
        {
        > 0 => new Vector3(Mathf.Abs(transformLocalScale.x), transformLocalScale.y, transformLocalScale.z),
        < 0 => new Vector3(-Mathf.Abs(transformLocalScale.x), transformLocalScale.y, transformLocalScale.z),
        _ => localScale
        };
        transform.localScale = localScale;  

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        UpdateAnimation();

        updateScore();
    }

    private void updateScore() {
        if(rigidBody.velocity.y > 0 && transform.position.y > topScore) 
        {
            topScore = fishCollected ? transform.position.y + 10 : transform.position.y;
            finalScore = topScore;
            fishCollected = false;
        }

        scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
    }

    private void UpdateAnimation()
    {
        MovementState state;

        switch (dirX)
        {
            case > 0f:
            case < 0f:
                state = MovementState.Running;
                break;
            default:
                state = MovementState.Idle;
                break;
        }

        state = rigidBody.velocity.y switch
        {
            > .1f => MovementState.Jumping,
            < -.1f => MovementState.Falling,
            _ => state
        };

        anim.SetInteger("state",(int)state);
    }

    private bool IsGrounded()
    {
        var bounds = coll.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public static void fishCollecter() {
        fishCollected = true;
    }
}
