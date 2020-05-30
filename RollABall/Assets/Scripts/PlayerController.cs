using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const string PickupTag = "pickup";

    public float ForceMultiplier;
    public Text ScoreText;
    public Text WinText;
    public GameObject Area;

    private Rigidbody PlayerObject;

    private int score = 0;
    private int maxScore = 0;

    private void Start()
    {
        PlayerObject = GetComponent<Rigidbody>();
        var pickups = GameObject.FindGameObjectsWithTag(PickupTag);

        WinText.text = "";
        maxScore = pickups.Count();
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

        Vector3 movement = default;
        if (moveHorizontal != 0)
        {
            movement = Camera.main.transform.right * moveHorizontal;
        }

        if (moveVertical != 0)
        {
            movement = Camera.main.transform.forward * moveVertical;
        }

        PlayerObject.AddForce(movement * ForceMultiplier);
    }

    // Detecs collision between THIS object and "other"
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PickupTag))
        {
            other.gameObject.SetActive(false);
            SetScoreText(++score);
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
