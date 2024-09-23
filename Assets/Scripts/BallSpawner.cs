using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    public Toggle tracer;
    public Toggle sound;
    public Toggle shake;
    public Toggle particles;

    [SerializeField] private Transform club;

    [SerializeField] private float clubSpawnOffset = 20;
    [SerializeField] private float clubSwingOffset = 90;

    [SerializeField] private int maxBallForce;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Shaker lclShaker;

    [SerializeField] int minForce = 100;

    [SerializeField] AudioSource hitSFXManager;

    [SerializeField] ParticleSystem dirt;

    private Vector3 clubDirection;
    private float swing = 0;
    [SerializeField]  private int ballForce;
    private GameObject ball;

    public bool isBigHit;

    float newBallTimer;

    private TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxBallForce;
        ball = Instantiate(ballPrefab, club.position, Quaternion.identity);
        trail = ball.GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;

        tracer = GameObject.Find("Tracer Toggle").GetComponent<Toggle>();
        sound = GameObject.Find("Sound Toggle").GetComponent<Toggle>();
        shake = GameObject.Find("Screen Shake Toggle").GetComponent<Toggle>();
        particles = GameObject.Find("Particle Toggle").GetComponent<Toggle>();
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

            if (ballForce >= (maxBallForce + minForce) / 1.5)
            {
                if (tracer.isOn)
                    trail.enabled = true;
                if (sound.isOn)
                    isBigHit = true;
                if (shake.isOn)
                    lclShaker.start = true;
            }
            else
            {
                isBigHit = false;
                lclShaker.start = false;
            }

            if (ballForce <= (maxBallForce + minForce) * .25)
            {
                if (particles.isOn)
                    dirt.Play();
            }
        }

        if (Mathf.Abs(ball.transform.position.x - club.position.x) >= 1)
        {
            ball = Instantiate(ballPrefab, club.position, Quaternion.identity);
            trail = ball.GetComponentInChildren<TrailRenderer>();
            trail.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ballForce = Mathf.Abs(Mathf.RoundToInt(Mathf.Sin(Time.time) * maxBallForce)) + minForce;
        slider.value = ballForce - minForce;
    }
}
