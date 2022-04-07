using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        OnGround,
        InAir,
        InWater
    }
    [SerializeField]
    public PlayerState currentState;

    public float moveSpeed = 2.0f;
    public Transform movePoint;

    public Transform playerSprite;
    public Animator playerAnim; //             Spencer added this line
    public SpriteRenderer sprite; //             Spencer added this line
    private Vector3 spriteSize;

    public KeyCode jumpKey = KeyCode.Space;
    public float jumpHeight;
    public float jumpTime;

    // Spencer added this block
    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = true;

    private void Start()
    {
        currentState = PlayerState.OnGround;
        movePoint.parent = null;
        spriteSize = playerSprite.localScale;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 0f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                // Spencer added this block
                if (Input.GetAxisRaw("Horizontal") < 0)
                    State("left");
                else
                    State("right");

            }else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                // Spencer added this block
                if (Input.GetAxisRaw("Vertical") < 0)
                    State("down");
                else
                    State("up");

            }
            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
                Debug.Log("Moving diagnal");
            }
        }

        //Jumping
        if(currentState == PlayerState.OnGround && Input.GetKeyDown(jumpKey))
        {
            StartCoroutine(Jump());
        }

        //Animation State
        Animation();
    }

    IEnumerator Jump()
    {
        currentState = PlayerState.InAir;
        Vector3 jumpSize = new Vector3(playerSprite.transform.localScale.x + jumpHeight, playerSprite.transform.localScale.y + jumpHeight,
            playerSprite.transform.localScale.z + jumpHeight);
        while (playerSprite.transform.localScale != jumpSize)
        {
            playerSprite.transform.localScale = Vector3.Lerp(playerSprite.transform.localScale, jumpSize, Time.deltaTime * jumpTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        while (playerSprite.transform.localScale != spriteSize)
        {
            playerSprite.transform.localScale = Vector3.Lerp(playerSprite.transform.localScale, spriteSize, Time.deltaTime * jumpTime);
            yield return null;
        }
        currentState = PlayerState.OnGround;
        print("Jump Complete");
    }


    //Changes the animation states                 Spencer added this function
    private void Animation()
    {
        // Left
        if (left == true)
            playerAnim.SetBool("MovingLeft", true);
        else
            playerAnim.SetBool("MovingLeft", false);

        // Right
        if (right == true) {
            playerAnim.SetBool("MovingRight", true);
            sprite.flipX = true; }
        else {
            playerAnim.SetBool("MovingRight", false);
            sprite.flipX = false; }

        // Up
        if (up == true)
            playerAnim.SetBool("MovingUp", true);
        else
            playerAnim.SetBool("MovingUp", false);

        // Down
        if (down == true)
            playerAnim.SetBool("MovingDown", true);
        else
            playerAnim.SetBool("MovingDown", false);
    }

    //Sets the move direction state               Spencer added this function
    private void State(string state)
    {
        left = false;
        right = false;
        up = false;
        down = false;

        if (state == "left")
            left = true;
        else if (state == "right")
            right = true;
        else if (state == "up")
            up = true;
        else if (state == "down")
            down = true;
    }
}
