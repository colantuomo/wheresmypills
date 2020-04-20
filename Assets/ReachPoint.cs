using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPoint : MonoBehaviour
{
    private GameManager gm;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!gm.getThePill)
        {
            spriteRenderer.color = Color.red;
        } else
        {
            spriteRenderer.color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (gm.getThePill)
            {
                gm.NextScene();
            }
        }
    }
}
