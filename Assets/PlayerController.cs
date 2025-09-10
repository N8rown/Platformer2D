using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PhysicsObject

{
    public float speed;
    public float jumpHeight;
    bool onMovingPlatform = false;
    float xAdjustment = 0f;
    int lives;
    public Text livesText;

    public Vector3 starting_position;
    void Start()
    {
        lives = 3;
        starting_position = new Vector3(-7, 21, 0);
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
            desiredx += 2f * xAdjustment;
    }
    override
    public void CollideWith(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            transform.position = starting_position;
            lives -= 1;
            livesText.text = lives.ToString();
        }
        if (other.CompareTag("Victory"))
        {
            Debug.Log("Victory!");
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
