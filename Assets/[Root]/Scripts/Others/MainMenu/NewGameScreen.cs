using UnityEngine;
using UnityEngine.UI;

public class NewGameScreen : MonoBehaviour , IScreenInit
{
    [Header("Buttons")]
    [SerializeField] private Button _onHistory;
    [SerializeField] private Button _onCreative;
    [SerializeField] private Button _onBack;


    [Space(10), Header("Screens")] //Views
    [SerializeField] private HistoryScreen _historyScreen;
    [SerializeField] private StartMenuScreen _startMenuScreen;
  

    private IScreenInit _cachedScreen;

    public void Dispose()
    {
        this.gameObject.SetActive(false);

        _onHistory.onClick.RemoveAllListeners();
        _onBack.onClick.RemoveAllListeners();
    }
    
    public void Initialize(IScreenInit hidenObj) {

        _cachedScreen = hidenObj;
        _cachedScreen.Dispose();

        this. gameObject.SetActive(true);
        _onBack.onClick.AddListener(() => _startMenuScreen.Initialize(this));
        _onHistory.onClick.AddListener(() => _historyScreen.Initialize(this));

    }
    private void OnDestroy() => Dispose();
}
 
