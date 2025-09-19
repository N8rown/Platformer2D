using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject

{
    public float speed;
    public float jumpHeight;
    bool onMovingPlatform = false;
    float xAdjustment = 0f;
    int lives;
    public Text livesText;
    int rounds_completed = 0;
    int points = 0;

    public Text pointsText;

    public Vector3 starting_position;
    void Start()
    {
        lives = 3;
        starting_position = new Vector3(-7, 21, 0);
        transform.position = starting_position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
            desiredx = speed;
        else if (Input.GetAxis("Horizontal") < 0)
            desiredx = -1 * speed;
        else
            desiredx = 0;

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = jumpHeight;
        if (onMovingPlatform)
            desiredx += 1f * xAdjustment;
    }
    override
    public void CollideWith(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            transform.position = starting_position;
            lives -= 1;
            livesText.text = lives.ToString();
            if (lives < 0)
            {
                lives = 3;
                
                SceneManager.LoadScene("StartMenu");
                rounds_completed = 0;
                livesText.text = lives.ToString();
                pointsText.text = rounds_completed.ToString();
                Debug.Log("Restarting Game");
            }
        }
        if (other.CompareTag("Points"))
        {
            points += 1;
            pointsText.text = points.ToString();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Victory"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            rounds_completed += 1;
            if (rounds_completed == 3)
            {
                Debug.Log("You Win!");
            }
        }
    }
    override
    public void CollideWithVertical(Collider2D other)
    {
        if (other.CompareTag("moving"))
        {
            xAdjustment = other.GetComponent<MovingPlatform>().desiredx;
            onMovingPlatform = true;
        }
        else
        {
            onMovingPlatform = false;
            xAdjustment = 0f;
        }
    }
}
