using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour , IScreenInitializer
{
    [Header("Buttons")]
    [SerializeField] private Button _onStart;
    [SerializeField] private Button _onSettings;
    [SerializeField] private Button _onShop;
    [SerializeField] private Button _onAutors;

    [Space(10), Header("Screens")]
    [SerializeField] private StartMenuScreen _startScreen;
    [SerializeField] private MainMenuSettingsScreen _settingsScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private AutorsMenuScreen _autorsScreen;
    public void Dispose()
    {
        this.gameObject.SetActive(false);
        _onSettings.onClick.RemoveAllListeners();
        _onShop.onClick.RemoveAllListeners();
        _onStart.onClick.RemoveAllListeners();
        _onAutors.onClick.RemoveAllListeners();
    }
    
    public void Initialize(IScreenInitializer hidenObj) { 
    
         if(hidenObj != null)
            hidenObj.Dispose();

        this.gameObject.SetActive(true);
        _onSettings.onClick.AddListener(() => _settingsScreen.Initialize(this));
        _onShop.onClick.AddListener(() => _shopScreen.Initialize(this));
        _onStart.onClick.AddListener(() => _startScreen.Initialize(this));
        _onAutors.onClick.AddListener(() => _autorsScreen.Initialize(this));
    }

    private void OnDestroy() => Dispose();

    
    private void Awake() => Initialize(null);
     
}
 
