using UnityEngine;

public class LoaderScenceOnMemory : MonoBehaviour
{
    [SerializeField] private InterectiveText interectiveText;


    private void Start()
    {
        if(interectiveText != null)
        {
            interectiveText.LoadSceneAsync();
        }
    }
}
