using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour 
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float timeInSeconds;


    [SerializeField] private Button _gameButton;
    [SerializeField] private GameObject _loadScreen;
    [SerializeField] private GameObject _menuScreen;

    private LoadSceneNotifire _notifire;
    private float time = 0;
    private AsyncOperation scene;


    private void Awake()
    {
        _gameButton.onClick.AddListener(() => LoadAsync());


    }
        
    
    private async void LoadAsync()
    {
        _menuScreen.SetActive(false);
        _loadScreen.SetActive(true);

        _notifire = new LoadSceneNotifire(timeInSeconds);
        
        _slider.maxValue = timeInSeconds;
        _slider.value = 0;
       
        scene = SceneManager.LoadSceneAsync(1);
       
        scene.allowSceneActivation = false; 
        Debug.Log("Успешно! ад!");

        await _notifire;
        Debug.Log("Успешно!");
        
        scene.allowSceneActivation = true;
    }
    private void Update()
    {
        if (_notifire == null) return;
        time += Time.deltaTime;
        _notifire.SetValue(time);
        _slider.value = time;
      
    }
}
public class LoadSceneNotifire : IAwatable<AsyncExt.Void>
{
    public Action<float> onCompleted;
    private float _maxTime;

    public LoadSceneNotifire(float maxTime) =>_maxTime = maxTime;

    public void SetValue(float time) => onCompleted?.Invoke(time);
    public IAwaiter<AsyncExt.Void> GetAwaiter() => new LoadSceneAsyncOperation(this, _maxTime);

    public class LoadSceneAsyncOperation : IAwaiter<AsyncExt.Void>
    {
        private bool _isCompleted;
        private float _maxValue;
        private Action _continue;
        private LoadSceneNotifire _notifire;
        public bool IsCompleted => _isCompleted;

        public LoadSceneAsyncOperation(LoadSceneNotifire notifire, float maxTimeOnComplete)
        {
            _notifire = notifire;
            _maxValue = maxTimeOnComplete;
            _notifire.onCompleted += onCompleted;
        }
        private void onCompleted(float currentValue)
        {
            if(currentValue >= _maxValue)
            {
                _notifire.onCompleted -= onCompleted;
                _isCompleted = true;
                _continue?.Invoke();
            }
        }
         
        public AsyncExt.Void GetResult() => new AsyncExt.Void();
         

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continue = continuation;
            }
        }
    }

}
