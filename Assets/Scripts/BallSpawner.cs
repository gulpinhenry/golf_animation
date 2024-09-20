using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Transform club;

    [SerializeField] private float clubSpawnOffset = 20;
    [SerializeField] private float clubSwingOffset = 90;

    [SerializeField] private int maxBallForce;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] int minForce = 10;

    [SerializeField] private GameObject trail;

    private Vector3 clubDirection;
    private float swing = 0;
    private int ballForce;
    private GameObject ball;
    private TrailRenderer trailRender;

    float newBallTimer;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxBallForce;
        ball = Instantiate(ballPrefab, club.position, Quaternion.identity);
        trailRender = trail.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        clubDirection = Input.mousePosition - club.position;
        club.transform.right = clubDirection;
        Vector3 clubRotation = club.transform.rotation.eulerAngles;

        swing += Time.deltaTime;

        if (swing >= 0)
            club.rotation = Quaternion.Euler(clubRotation.x, clubRotation.y, clubRotation.z - clubSwingOffset);

        if (Input.GetMouseButtonDown(0))
        {
            ball.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
            rb.AddForce(clubDirection.normalized * ballForce);
            club.rotation = Quaternion.Euler(clubRotation.x, clubRotation.y, clubRotation.z + clubSpawnOffset);
            swing = -(45 * Time.deltaTime);
            if (trailRender != null)
            {
                if (ballForce > maxBallForce * .75)
                {
                    trailRender.enabled = true;
                }
            }
        }

        if (Mathf.Abs(ball.transform.position.x - club.position.x) >= 1)
        {
            ball = Instantiate(ballPrefab, club.position, Quaternion.identity);
            trailRender = ball.GetComponentInChildren<TrailRenderer>();
            trailRender.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ballForce = Mathf.Abs(Mathf.RoundToInt(Mathf.Sin(Time.time) * maxBallForce)) + minForce;
        slider.value = ballForce - minForce;
    }
}
