using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
    // Start is called before the first frame update
    public float enemySpeed = 2f;
    void Start()
    {
        desiredx = enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    override
    public void CollideWithHorizontal(Collider2D other)
    {
        desiredx = -desiredx;
    }
}
