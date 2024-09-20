using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class Trail_Behavior : MonoBehaviour
{
    [SerializeField] public GameObject ball;

    [SerializeField] public GameObject ground;

    public Collider2D groundCollider;
    public Collider2D ballCollider;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = new Vector3(ball.transform.position.x, ball.transform.position.y / 2, 0);
        ballCollider = ball.GetComponent<Collider2D>();
        groundCollider = ground.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(ground.transform.position.y);

        print(ballCollider.IsTouching(groundCollider));

        if (ballCollider.IsTouching(groundCollider))
        {
            ball.GetComponent<TrailRenderer>().enabled = false;
        }
        else
        {
            ball.GetComponent<TrailRenderer>().enabled = true;
        }
    }
}
