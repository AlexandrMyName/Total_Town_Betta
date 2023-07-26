using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettingsScreen : MonoBehaviour , IScreenInit
{
    [Header("Buttons")]
    [SerializeField] private Button _onAudio;
    [SerializeField] private Button _onGraphics;
    [SerializeField] private Button _onInput;
    [SerializeField] private Button _onLocale;
    [SerializeField] private Button _onBack;


    [Space(10), Header("Screens")] //Views
    [SerializeField] private Audio_SettingsScreen _audioScreen;
    [SerializeField] private MainMenuScreen _menuScreen;
    [SerializeField] private GameObject _inputScreen;
    [SerializeField] private LocalizationScreen _localizationScreen;

    public void Dispose()
    {
        this.gameObject.SetActive(false);

        _onGraphics.onClick.RemoveAllListeners();
        _onInput.onClick.RemoveAllListeners();
        _onAudio.onClick.RemoveAllListeners();
        _onBack.onClick.RemoveAllListeners();
    }
    
    public void Initialize(IScreenInit hidenObj) {


        hidenObj.Dispose();

        this. gameObject.SetActive(true);
        _onBack.onClick.AddListener(() => _menuScreen.Initialize(this));
        _onAudio.onClick.AddListener(() => _audioScreen.Initialize(this));
        _onLocale.onClick.AddListener(() => _localizationScreen.Initialize(this));
    }
    private void OnDestroy() => Dispose();
}
 
