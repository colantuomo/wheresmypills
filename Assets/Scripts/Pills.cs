using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Pills : MonoBehaviour
{
    private readonly float magnitude = 4f;
    private readonly float roughness = 2f;
    private readonly float fadeInTime = .1f;
    private readonly float fadeOutTime = 1f;
    public ParticleSystem catchPillFX;

    private GameManager gm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gm.GetThePill();
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
            Instantiate(catchPillFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
