using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class SceneLoaderScreen : MonoBehaviour, IScreenInitializer
{
    [SerializeField] private string assetSceneID_onNewGame;
    [SerializeField] private Button _onApply;
    [SerializeField] private TMP_Text _textPercent;
    [SerializeField] private GameObject _hidenObject;
    public class LoadSceneNotifire : IAwatable<AsyncExt.Void>
    {
        public Action<float> onCompleted;
        private float _maxTime;

        public LoadSceneNotifire(float maxTime) => _maxTime = maxTime;

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
                if (currentValue >= _maxValue)
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

  

    [SerializeField] private Slider _loaderSlider;
    [SerializeField] private float timeInSeconds;

    private LoadSceneNotifire _notifire;
    private AsyncOperationHandle<SceneInstance> sceneMemory;

    private void LoadAsync()
    {
        _hidenObject.SetActive(false);
        _loaderSlider.maxValue = 100;
        _loaderSlider.value = 0;

        sceneMemory = Addressables.LoadSceneAsync(assetSceneID_onNewGame,LoadSceneMode.Single,false);
        _notifire = new LoadSceneNotifire(timeInSeconds);
        sceneMemory.Completed += ActivateButtonApply;
        
    }

    private void ActivateButtonApply(AsyncOperationHandle<SceneInstance> sceneAsyncOperation)
    {
        _onApply.onClick.AddListener(ActivateScene);
        _notifire = null;
        _textPercent.text = "100 %";
        _onApply.interactable = true;
        
    }
    private void ActivateScene()
    {
        sceneMemory.Result.ActivateAsync();
    }
    
    private void Update()
    {
        if (_notifire == null) return;


        var percent = sceneMemory.PercentComplete;
        int roundPercent = Mathf.RoundToInt(percent);
        _textPercent.text = $"{roundPercent} %";
        _loaderSlider.value = roundPercent;

    }

    public void Dispose() {

        _onApply.onClick.RemoveAllListeners();
    
    }

    public void Initialize(IScreenInitializer hidenObj)
    {
        hidenObj.Dispose();
        this.gameObject.SetActive(true);
        LoadAsync();
    }
}
