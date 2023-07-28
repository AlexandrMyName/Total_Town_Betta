using UnityEngine;
using Zenject;

public class LoaderScenceOnMemory : MonoBehaviour
{
    [Inject] private InterectiveText _interectiveText;


    [Inject]
    private void Constract(InterectiveText interectiveText)
    {
        _interectiveText = interectiveText;
        
    }
    private void Start()
    {
        if (_interectiveText != null)
        {
            _interectiveText.LoadSceneAsync(this);
        }
    }
}
