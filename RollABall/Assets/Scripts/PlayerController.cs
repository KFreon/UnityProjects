using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    struct Pickup
    {
        public string Name { get; set; }
        public float SizeAdjustment { get; set; }

        public Pickup(string name, float sizeAdjustment)
        {
            Name = name;
            SizeAdjustment = sizeAdjustment;
        }

    }

    private Pickup[] Pickups;

    const string SweetPickupTag = "SweetPickup";
    const string SavouryPickupTag = "SavouryPickup";

    public float ForceMultiplier;
    public float JumpPower;
    public Text ScoreText;
    public Text WinText;
    public float SizeAdjustment;

    public List<GameObject> ActivePickups = new List<GameObject>();

    private Rigidbody PlayerObject;

    private int score = 0;
    private int maxScore = 0;

    private void Start()
    {
        Pickups = new[]
        {
            new Pickup(SweetPickupTag, -1f * SizeAdjustment),
            new Pickup(SavouryPickupTag, SizeAdjustment)
        };

        PlayerObject = GetComponent<Rigidbody>();
        foreach(var pickup in Pickups)
        {
            maxScore += GameObject.FindGameObjectsWithTag(pickup.Name).Length;
        }

        WinText.text = "";
        SetScoreText(score);
    }

    // Before frame
    private void Update()
    {
        // Handle particles
        foreach (var pickup in ActivePickups)
        {
            var particles = pickup.GetComponent<ParticleSystem>();
            if (particles == null)
            {
                return;
            }

            if (!particles.isEmitting && pickup.activeSelf)
            {
                pickup.SetActive(false);
            }
        }
    }

    // Before physics calcs
    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var jump = Input.GetKeyDown(KeyCode.Space);

        Vector3 movement = default;
        if (moveHorizontal != 0)
        {
            movement = Camera.main.transform.right * moveHorizontal;
        }

        if (moveVertical != 0)
        {
            movement = Camera.main.transform.forward * moveVertical;
        }

        if (jump)
        {
            movement.y += JumpPower;
        }

        PlayerObject.AddForce(movement * ForceMultiplier);
    }

    // Detects collision between THIS object and "other"
    private void OnTriggerEnter(Collider other)
    {
        foreach(var pickup in Pickups)
        {
            if (other.gameObject.CompareTag(pickup.Name))
            {
                //ActivePickups.Add(other.gameObject);
                SetScoreText(++score);
                PlayerObject.transform.localScale *= (1 + pickup.SizeAdjustment);
            }
        }
    }

    private void SetScoreText(int score)
    {
        ScoreText.text = $"Score: {score} / {maxScore}";

        if (score >= maxScore)
        {
            WinText.text = "Winnaaaaa!!";
        }
    }
}
