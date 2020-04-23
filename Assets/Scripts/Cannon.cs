using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingPoint;
    public Transform aimPoint;
    public float shootSpeed = 3f;

    private List<GameObject> bullets;
    private float timer = 0f;
    private float timerDelay = 1.2f;
    void Start()
    {
        bullets = new List<GameObject>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(Time.time > timer)
        {
            timer = Time.time + timerDelay;
            GameObject bulletRef = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
            bullets.Add(bulletRef);
        }

        bullets.ForEach(b =>
        {
            if(b != null)
                b.transform.position = Vector3.MoveTowards(b.transform.position, aimPoint.position, shootSpeed * Time.deltaTime);
        });
    }
}
