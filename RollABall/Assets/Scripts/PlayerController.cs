using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float ForceMultiplier;
    public Text ScoreText;
    public Text WinText;

    private Rigidbody PlayerObject;

    private int score = 0;

    private void Start()
    {
        PlayerObject = GetComponent<Rigidbody>();
        SetScoreText(score);
        WinText.text = "";
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

        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        PlayerObject.AddForce(movement * ForceMultiplier);
    }

    // Detecs collision between THIS object and "other"
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            SetScoreText(++score);
        }
    }

    private void SetScoreText(int score)
    {
        ScoreText.text = $"Score: {score}";

        if (score > 5)
        {
            WinText.text = "Winnaaaaa!!";
        }
    }
}
