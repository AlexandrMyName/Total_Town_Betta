using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HistoryScreen : MonoBehaviour , IScreenInit
{
    [Header("Buttons/Inputs")]
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private Button _onApply;
    [SerializeField] private Button _onBack;
    [SerializeField] private UserProfile _userDataProfile;

    [Space(10), Header("Screens")] //Views
    [SerializeField] private StartMenuScreen _startMenuScreen;
    [SerializeField] private SceneLoaderScreen _sceneLoader;
    [SerializeField] private GameObject _inputScreen;

    private IScreenInit _cachedScreen;

    public void Dispose()
    {
        this.gameObject.SetActive(false);

       
        _onBack.onClick.RemoveAllListeners();
    }
    
    public void Initialize(IScreenInit hidenObj) {

        _cachedScreen = hidenObj;
        _cachedScreen.Dispose();

        this. gameObject.SetActive(true);
        _onBack.onClick.AddListener( () => _cachedScreen.Initialize(this));
        _onApply.onClick.AddListener(StartHistoryGame);
    }

    private void StartHistoryGame()
    {
        if(_nameField.text != string.Empty)
        {
            _sceneLoader.Initialize(this);
        }
    }
    private void OnDestroy() => Dispose();

    private void Update()
    {
        if (_nameField.text.Length < 3)
        {
            _onApply.interactable = false;
        }
        else
        {
            _userDataProfile.UserName = _nameField.text;
            _onApply.interactable = true;

        }
    }
}
 
