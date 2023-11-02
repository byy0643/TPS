using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform center;
    public float radius = 15.0f;
    public float speed = 1.0f;
    public Vector3 forwardDir;
    public GameObject gameObject;
    public Vector3[] targets;
    public Animator animator;

    public int hp = 5;

    private float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //angle += speed * Time.deltaTime;
        //transform.position = center.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        
    }

    public void Hit()
    {
        if(hp>0)
        {
            hp--;
        }
        else
        {
            Die();
        }
    }
    public void Die()
    {
        animator.SetTrigger("Die");
    }
}
