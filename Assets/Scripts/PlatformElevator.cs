using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElevator : MonoBehaviour
{
    public Transform pointToMove;
    public float speed = 5f;

    private Vector3 initialPosition;
    private bool playerEnter;
    private GameManager gm;
    void Start()
    {
        initialPosition = transform.position;
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (playerEnter)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove.position, speed * Time.deltaTime);
        }

        if(gm.hasRespawn)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        playerEnter = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            playerEnter = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
        }
    }
}
