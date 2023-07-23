using UnityEngine;
using UnityEngine.UI;


public class GoToURL : MonoBehaviour
{
    [SerializeField] private string _url;
    [SerializeField] private Button _button;

    private void Awake() => _button.onClick.AddListener(() => GoTo(_url));
    
    public void GoTo(string url = null) => Application.OpenURL(url);

    private void OnDestroy() =>  _button.onClick.RemoveAllListeners();
    
}
