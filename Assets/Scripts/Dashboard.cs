using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private DashboardData dashboardData;
    [SerializeField] private UIDocument document;
    private Toggle musicToggle;
    private Toggle sfxToggle;
    private Toggle saveToggle;
    private Toggle backgroundToggle;
    private Toggle cardBGToggle;
    private Button startButton;
    void Start()
    {
        var rootElement = document.rootVisualElement;

        musicToggle = rootElement.Q<Toggle>("MusicToggle");

        musicToggle.value = dashboardData.allowMusic;

        musicToggle.RegisterValueChangedCallback(OnMusicToggleValueChanged);

        sfxToggle = rootElement.Q<Toggle>("SFXToggle");

        sfxToggle.value = dashboardData.allowSFX;

        sfxToggle.RegisterValueChangedCallback(OnSFXToggleValueChanged);

        saveToggle = rootElement.Q<Toggle>("SaveToggle");

        saveToggle.value = dashboardData.canSave;

        saveToggle.RegisterValueChangedCallback(OnSaveToggleValueChanged);

        backgroundToggle = rootElement.Q<Toggle>("BackgroundToggle");

        backgroundToggle.value = dashboardData.changeBackground;

        backgroundToggle.RegisterValueChangedCallback(OnBackgroundToggleValueChanged);

        cardBGToggle = rootElement.Q<Toggle>("CardBGToggle");

        cardBGToggle.value = dashboardData.changeCardBackground;

        cardBGToggle.RegisterValueChangedCallback(OnCardBGToggleValueChanged);

        startButton = rootElement.Q<Button>("StartButton");

        startButton.clickable.clicked += OnStartButtonClicked;
    }

    private void OnCardBGToggleValueChanged(ChangeEvent<bool> evt)
    {
        dashboardData.changeCardBackground = evt.newValue;
    }

    private void OnBackgroundToggleValueChanged(ChangeEvent<bool> evt)
    {
        dashboardData.changeBackground = evt.newValue;
    }

    private void OnSaveToggleValueChanged(ChangeEvent<bool> evt)
    {
        dashboardData.canSave = evt.newValue;
    }

    private void OnSFXToggleValueChanged(ChangeEvent<bool> evt)
    {
        dashboardData.allowSFX = evt.newValue;
    }

    private void OnMusicToggleValueChanged(ChangeEvent<bool> evt)
    {
        dashboardData.allowMusic = evt.newValue;
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene((int)Scenes.GameScene);
    }

    private void OnDestroy()
    {
        startButton.clickable.clicked -= OnStartButtonClicked;
    }

}
