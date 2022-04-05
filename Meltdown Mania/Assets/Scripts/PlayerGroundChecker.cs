using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGroundChecker : MonoBehaviour
{
    public PlayerController playerController;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Iceblock>() != null)
        {
            Iceblock iceData = collision.gameObject.GetComponent<Iceblock>();
            print(iceData.currentState);
            if(iceData.currentState == IceState.Melted && playerController.currentState == PlayerController.PlayerState.OnGround)
            {
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }
    }
}
