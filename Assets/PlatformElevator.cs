using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformElevator : MonoBehaviour
{
    public Transform pointToMove;
    public float speed = 5f;

    private bool playerEnter;
    void Start()
    {
        
    }

    void Update()
    {
        if (playerEnter)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove.position, speed * Time.deltaTime);
        }
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
