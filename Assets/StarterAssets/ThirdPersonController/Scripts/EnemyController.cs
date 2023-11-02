using cakeslice;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform center;
    public float radius = 15.0f;
    public float speed = 1.0f;
    public Vector3 forwardDir;
    public GameObject gameObject;

    private float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = GetComponent<GameObject>();
        forwardDir = gameObject.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        
    }
}
