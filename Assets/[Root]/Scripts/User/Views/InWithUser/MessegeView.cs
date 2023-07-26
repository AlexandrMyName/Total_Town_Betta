using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessegeView : MonoBehaviour, IProccess
{
    [SerializeField] private TMP_Text _messege;
    [SerializeField] private Image _icon;

    private bool _isActive;
    AsyncAwaiterTime _awaiter;
    public bool IsProccess => _isActive;

    private float currentTime;

    public async void SendMessageToUser(string messege, Sprite iconFrom, float maxTime = 5f)
    {
        _messege.text = messege;
        _icon.sprite = iconFrom;


        this.gameObject.SetActive(true);

         _awaiter = new AsyncAwaiterTime(maxTime);
        _isActive = true;

        await _awaiter;
        _awaiter = null;
        _isActive = false;
        currentTime = 0;
        this.gameObject.SetActive(false);
    }


    private void Update()
    {
        if(_isActive && _awaiter != null)
        {
            currentTime += Time.deltaTime;
            _awaiter.SetValue(currentTime);
        }
    }
}
