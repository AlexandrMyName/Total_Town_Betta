using UnityEngine;
using Zenject;

public class LoaderScenceOnMemory : MonoBehaviour
{
    [Inject] private InterectiveText _interectiveText;


    [Inject]
    private void Constract(InterectiveText interectiveText)
    {
        _interectiveText = interectiveText;
        if(interectiveText != null)
        {
            interectiveText.LoadSceneAsync(this);
        }
    }
}
