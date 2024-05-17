using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sumText;
    [SerializeField] private GameObject endScreen;
    private void Start()
    {
        endScreen.SetActive(false);
    }

    public void UpdateSumText(string text)
    {
        sumText.text = "Sum : " + text;
    }

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);
    }
}
