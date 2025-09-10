using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PhysicsObject
{
    public float speed = 10f;

    void Start()
    {
        desiredx = speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    override
     public void CollideWithHorizontal(Collider2D other)
    {
        StartCoroutine(changeDirectionDelayed());
        
    }
    IEnumerator changeDirectionDelayed()
    {
        float temp = desiredx;
        desiredx = 0;
        yield return new WaitForSeconds(1.5f);
        desiredx = -temp;
    }

}
