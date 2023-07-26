using UnityEngine;
using UnityEngine.UI;

public class Hint_P : MonoBehaviour
{
    [SerializeField] private Button _button;

    [SerializeField] private GameObject _hint;
    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            _hint.SetActive(true);
            Destroy(_hint, 5);
            _button.onClick.RemoveAllListeners();
            Destroy(_button.gameObject,5); 
        });
             
    }
}
