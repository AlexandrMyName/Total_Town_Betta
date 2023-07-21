using UnityEngine;

public class EffectsView : MonoBehaviour 
{
    [SerializeField] private GameObject _onSelectEffect;



    public void ActiveSelectedEffect()  
    {
        _onSelectEffect.SetActive(true);
    }
    public void DeactiveSelectedEffect()
    {
        _onSelectEffect.SetActive(false);
    }
}
