using UnityEngine;
using UnityEngine.UI;

public class StartMenuScreen : MonoBehaviour , IScreenInit
{
    [Header("Buttons")]
    [SerializeField] private Button _onNewGame;
    [SerializeField] private Button _onLoadGame;
 
    [SerializeField] private Button _onBack;
     

    [Space(10), Header("Screens")] //Views
    [SerializeField] private NewGameScreen _newGameScreen;
    [SerializeField] private MainMenuScreen _menuScreen;
    [SerializeField] private GameObject _inputScreen;

    private IScreenInit _cachedScreen;

    public void Dispose()
    {
        this.gameObject.SetActive(false);

        _onNewGame.onClick.RemoveAllListeners();
        _onLoadGame.onClick.RemoveAllListeners();
        _onBack.onClick.RemoveAllListeners();
    }
    
    public void Initialize(IScreenInit hidenObj) {

        _cachedScreen = hidenObj;
        _cachedScreen.Dispose();

        this. gameObject.SetActive(true);
        _onBack.onClick.AddListener(() => _menuScreen.Initialize(this));
        _onNewGame.onClick.AddListener(()=> _newGameScreen.Initialize(this));
        
    }
    private void OnDestroy() => Dispose();
}
 
