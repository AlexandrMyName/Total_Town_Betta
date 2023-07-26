using UnityEngine;
using UnityEngine.UI;

public class LocalizationScreen : MonoBehaviour, IScreenInit
{
    [SerializeField] private Button _onBack;
    [SerializeField] private MainMenuSettingsScreen _mainMenuSettingsScreen;

    public void Dispose()
    {
        this.gameObject.SetActive(false);
        _onBack.onClick.RemoveAllListeners();
    }

    public void Initialize(IScreenInit hidenObj)
    {
        hidenObj.Dispose();

        this.gameObject.SetActive(true);

        _onBack.onClick.AddListener(() => _mainMenuSettingsScreen.Initialize(this));
    }
}
