using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private FallTrigger[] fallTriggers;
    private GameObject Pins;
    [SerializeField] private BallController ball;

    [SerializeField] private GameObject pinCollection;

    [SerializeField] private Transform pinAnchor;

    [SerializeField] private InputManager inputManager;
    private void Start()
    {
        fallTriggers = FindObjectsByType<FallTrigger>((FindObjectsSortMode)FindObjectsInactive.Include);

        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();

    }
    private void HandleReset()
    {
        ball.ResetBall();
        SetPins();
    }
    private void SetPins()
    {
        if (Pins)
        {
            foreach (Transform child in Pins.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(Pins);
        }

        Pins = Instantiate(pinCollection, pinAnchor.transform.position, Quaternion.identity, transform);

        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }
    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
