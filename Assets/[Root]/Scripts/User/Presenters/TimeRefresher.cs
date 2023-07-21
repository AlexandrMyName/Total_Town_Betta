using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class TimeRefresher : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    
    [Inject]
    private void Init(ITimer timer)
    {
        var r = timer.GameTime.Subscribe(sec =>
        {
            var t = TimeSpan.FromSeconds(sec);
            _timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",t.Hours, t.Minutes, t.Seconds);
        });

    }
}
