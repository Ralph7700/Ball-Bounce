using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform topright, bottomleft;
    public GameObject ballprefab;
    private float spawnX, spawnY;
    public float spawnOffset;
    private GameObject ball;
    public Color activeColor;

    private void Start()
    {
        ball = GameObject.Find("ball");
        SpawnNewBall();
        
    }
    public void SpawnNewBall()
    {
        if (ball != null)
        {
            ball.GetComponent<SpriteRenderer>().color = activeColor;
            ball.GetComponent<CircleCollider2D>().enabled = true;
        }
        do
        {
            spawnX = Random.Range(bottomleft.position.x, topright.position.x);
            spawnY = Random.Range(bottomleft.position.y, topright.position.y);
        }
        while ((ball.transform.position - new Vector3(spawnX, spawnY, 0f)).magnitude < spawnOffset);

        ball = Instantiate(ballprefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
    }

}
