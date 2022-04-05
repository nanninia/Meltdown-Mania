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
    private Vector3 spriteSize;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpHeight;
    public float jumpTime;

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

            }else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

            }else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
            }
        }

        //Jumping
        if(currentState == PlayerState.OnGround && Input.GetKeyDown(jumpKey))
        {
            StartCoroutine(Jump());
        }
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
}
