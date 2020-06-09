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

    readonly Pickup[] Pickups =
    {
        new Pickup(SweetPickupTag, -0.1f),
        new Pickup(SavouryPickupTag, 0.1f)
    };

    const string SweetPickupTag = "SweetPickup";
    const string SavouryPickupTag = "SavouryPickup";

    public float ForceMultiplier;
    public float JumpPower;
    public Text ScoreText;
    public Text WinText;
    public GameObject Area;

    private Rigidbody PlayerObject;

    private int score = 0;
    private int maxScore = 0;

    private void Start()
    {
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

    // Detecs collision between THIS object and "other"
    private void OnTriggerEnter(Collider other)
    {
        foreach(var pickup in Pickups)
        {
            if (other.gameObject.CompareTag(pickup.Name))
            {
                other.gameObject.SetActive(false);
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
