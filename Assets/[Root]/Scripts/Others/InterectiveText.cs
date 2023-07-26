using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using System.Threading.Tasks;
using UniRx;
using UnityEditor.Rendering;
using System;
using System.Collections;

/// <summary>
/// Интерактивная загрузка уровня с перечисляющим текстом
/// </summary>
public class InterectiveText : MonoBehaviour
{
    [SerializeField] private string textEnd;

    [SerializeField] private string _loadingSceneAssetID;
    [SerializeField] private GameObject _loadingSreenBetweenScence;
    
    [SerializeField] private TMP_Text _tmpText;

   

    private AsyncAwaiterTime _waiter;
    
    
    private float _currentTime;
    [SerializeField] private float _maxValueTimeInSeconds;

    [Header("Load perccent")]

    [SerializeField] private GameObject _loadGameObjectRotate;
    [SerializeField] private TMP_Text _loadPerccent;

    private AsyncOperation asyncOperation;
    private AsyncOperationHandle<SceneInstance> handler;

    public void LoadSceneAsync(MonoBehaviour activeMono)
    {
      activeMono.StartCoroutine(LoadScene());
       
    }

   
    private IEnumerator LoadScene()
    {
       handler = Addressables.LoadSceneAsync(_loadingSceneAssetID, LoadSceneMode.Single, false);

        while (!handler.IsDone)
        {
             
            ShowPercent();
            yield return null;
        }
        ShowPercent(true);
    }

    private void ShowPercent(bool iscomlete = false)
    {
        if (iscomlete) {
            _loadPerccent.text = $" 100 / 100";
            return;
        }
            DownloadStatus status = handler.GetDownloadStatus();
        float percent = status.Percent * 100;
        _loadPerccent.text = $"{Mathf.RoundToInt(percent)} / 100";
        
    }

    public async void PlayEnd()
    {
       
        _loadGameObjectRotate.SetActive(true);
        _loadPerccent.gameObject.SetActive(true);

      

        if (textEnd != null)
        {

             
            _tmpText.gameObject.SetActive(true);
            _waiter = new AsyncAwaiterTime(_maxValueTimeInSeconds);

            for(int i = 0; i < textEnd.Length; i++)
            {

                _tmpText.text += textEnd[i];
                await _waiter;
                _currentTime = 0;
            }
            _waiter = null;
            _currentTime = 0;
        }

        _tmpText.gameObject.SetActive(false);
        gameObject.GetComponent<Image>().enabled = false;

        HideAllScene();
    }
    
    private async void  HideAllScene()
    {
        _loadingSreenBetweenScence.SetActive(true);
        var canvas = _loadingSreenBetweenScence.GetComponent<Canvas>();
        
        while (canvas.planeDistance > 100)
        {
            canvas.planeDistance -= 1;
            _waiter = new AsyncAwaiterTime(_currentTime + 0.02f);
            await _waiter;
        }
        _loadGameObjectRotate.SetActive(false);
        _loadPerccent.text = "Пожалуйста подождите! Настройка контента..";

        if(handler.IsDone)
           handler.Result.ActivateAsync();
        else
        {
            await StartCoroutine(WaitSceneLoading());
        }
    }
   
    private IEnumerator WaitSceneLoading()
    {
        while (!handler.IsDone)
        {
            ShowPercent();
             yield return null;  
        }
        handler.Result.ActivateAsync();
    }
    private void Update()
    {
       
        if(_waiter != null)
        {
            _currentTime+= Time.deltaTime;
            _waiter.SetValue(_currentTime);
        }
    }
}
