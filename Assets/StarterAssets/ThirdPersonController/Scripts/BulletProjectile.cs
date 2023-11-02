using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbidy;

    private void Awake()
    {
        bulletRigidbidy = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float speed = 10f;
        bulletRigidbidy.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
