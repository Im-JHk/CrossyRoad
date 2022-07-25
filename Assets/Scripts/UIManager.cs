using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField]
    private GameObject gameoverUI;
    [SerializeField]
    private Text gameScore;
    [SerializeField]
    private Text coinScore;

    void Start()
    {
        gameoverUI.SetActive(false);
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
        gameoverUI.SetActive(active);
    }
}
