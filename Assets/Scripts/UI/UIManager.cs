using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField]
    private GameObject gameoverUI;
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject pauseButtonUI;
    [SerializeField]
    private Text gameScore;
    [SerializeField]
    private Text coinScore;

    void Start()
    {
        gameoverUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    public void UpdateTextGameScore(int score)
    {
        gameScore.text = score.ToString();
    }

    public void UpdateTextCoinScore(int score)
    {
        coinScore.text = score.ToString();
    }

    public void SetActiveGameoverUI(bool active)
    {
        pauseButtonUI.SetActive(!active);
        gameoverUI.SetActive(active);
    }

    public void SetActivePauseUI(bool active)
    {
        pauseUI.SetActive(active);
    }

    public void SetActivePauseButtonUI(bool active)
    {
        pauseButtonUI.SetActive(active);
    }
}
