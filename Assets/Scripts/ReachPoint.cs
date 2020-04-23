using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ReachPoint : MonoBehaviour
{
    private GameManager gm;
    private SpriteRenderer spriteRenderer;
    public Light2D light2D;
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
            light2D.color = Color.red;
        } else
        {
            spriteRenderer.color = Color.green;
            light2D.color = Color.green;
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
