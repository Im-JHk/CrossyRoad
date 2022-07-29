using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour
{
    [SerializeField]
    private Image logoImage = null;
    [SerializeField]
    private Text pressStartText = null;
    [SerializeField]
    private Button startButton = null;

    private float logoAlpha = 0f;
    private float textAlpha = 0f;
    private int textAlphaDirection;
    private bool isLogoDraw = false;

    private readonly float logoAlphaTickSpeed = 0.4f;
    private readonly float textAlphaTickSpeed = 0.8f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        startButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {

        Color logoColor = logoImage.color;
        Color textColor = pressStartText.color;
        logoColor.a = 0;
        textColor.a = 0;
        logoImage.color = logoColor;
        pressStartText.color = textColor;
        textAlphaDirection = 1;
        pressStartText.gameObject.SetActive(false);

        startButton.onClick.AddListener(LoadGameScene);
        startButton.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!isLogoDraw)
        {
            Color color = logoImage.color;

            logoAlpha += logoAlphaTickSpeed * Time.fixedDeltaTime;
            if(logoAlpha > 1f)
            {
                logoAlpha = Mathf.Clamp01(logoAlpha);
                isLogoDraw = true;
                pressStartText.gameObject.SetActive(true);
                startButton.gameObject.SetActive(true);
            }

            color.a = logoAlpha;
            logoImage.color = color;
        }
        else
        {
            Color color = pressStartText.color;

            textAlpha += textAlphaDirection * textAlphaTickSpeed * Time.fixedDeltaTime;
            if(textAlpha > 1f)
            {
                textAlphaDirection = -1;
                textAlpha = Mathf.Clamp01(textAlpha);
            }
            else if (textAlpha < 0)
            {
                textAlphaDirection = 1;
                textAlpha = Mathf.Clamp01(textAlpha);
            }

            color.a = textAlpha;
            pressStartText.color = color;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
