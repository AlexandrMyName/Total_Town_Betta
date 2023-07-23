using UnityEngine;
using UnityEngine.UI;

public class AutorsMenuScreen : MonoBehaviour, IScreenInitializer
{
    [Header("Buttons/Inputs")]
    [SerializeField] private Button _onBack;
    [SerializeField] private Scrollbar _scrollbar;

    [Space(10), Header("Screens")] //Views
    [SerializeField] private MainMenuScreen _mainMenuScreen;

    [SerializeField] private float _speedAutorScrollBar;

    private bool _initialized = true;
    public void Dispose()
    {

        this.gameObject.SetActive(false);
        _initialized = false;

        _onBack.onClick.RemoveAllListeners();
    }

    public void Initialize(IScreenInitializer hidenObj)
    {
        hidenObj.Dispose();
        
            this.gameObject.SetActive(true);
        _onBack.onClick.AddListener(() => _mainMenuScreen.Initialize(this));
        
    }

  
    private void OnDestroy() => Dispose();

  

    private void Update()
    {
        if (!_initialized) return;
        _scrollbar.value -= Time.deltaTime * _speedAutorScrollBar;
    }
}

