using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallBehaviour : MonoBehaviour
{
    private Vector2 firstPosition;
    private Vector2 secondPostion;
    private Vector2 direction;

    public float movementSpeed;
    public float minimumSwipe;

    public SpriteRenderer[] borderSprites = new SpriteRenderer[4];

    [SerializeField] private UnityEvent collectedball;
    public void Update()
    {
        // Inputs:

        if (Input.GetMouseButtonDown(0))
        {
            firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(0))
        {
            secondPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButtonUp(0))
        {
            if ((secondPostion - firstPosition).magnitude > minimumSwipe)
            {

                direction = secondPostion - firstPosition;
                direction.Normalize();
            }
        }

        // Mouvement

        transform.position = transform.position + new Vector3(direction.x * movementSpeed * Time.deltaTime, direction.y * movementSpeed * Time.deltaTime, 0f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //border collision
        if (collision.gameObject.layer == 6) 
        {
            direction.y = -direction.y;BorderHit();GameManager.Instance.OnBorderHit();
        }
        if (collision.gameObject.layer == 7) 
        {
            direction.x = -direction.x;BorderHit();GameManager.Instance.OnBorderHit();
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        collectedball.Invoke();
    }


    private void BorderHit()
    {
        switch (GameManager.Instance.borderHitCount)
        {
            case 0:
                for (int i = 0; i < 4; i++) { borderSprites[i].color = new Color(1f, 150/255f, 0f); }
                GameManager.Instance.borderHitCount += 1;break;
            case 1:
                for (int i = 0; i < 4; i++) { borderSprites[i].color = new Color(1f, 0f, 0f); }
                GameManager.Instance.borderHitCount += 1;break;
        }       
    }
}