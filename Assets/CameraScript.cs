using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : PlayerController
{
    public Transform player;
    private Vector3 startingPosition = new Vector3(13.5f, 0f, -10f);
    public float interval = 49.5f;
    private float playerX;
    private float playerY;
    private float startingX;//X at the start of level
    private float startingY; //Y at the start of jump
    public float yOffset = 7f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerX = player.position.x;
        playerY = player.position.y;
        startingX = startingPosition.x;
        if (playerX < startingX + interval)
            transform.position = new Vector3(startingX, playerY + yOffset, -10f);

        else if (playerX > startingX + interval && playerX < startingX + 3 * interval)
            transform.position = new Vector3(startingX + 2 * interval, playerY + yOffset, -10f);
            
        else if (playerX > startingX + 3 * interval && playerX < startingX + 5 * interval)
            transform.position = new Vector3(startingX + 4 * interval, playerY + yOffset, -10f);

        else if (playerX > startingX + 5 * interval && playerX < startingX + 7 * interval)
            transform.position = new Vector3(startingX + 6 * interval, playerY + yOffset, -10f);
    }
    
}
