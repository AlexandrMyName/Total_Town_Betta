using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour, IScreenInit
{
    [SerializeField] private Button _onBack;
    [SerializeField] private MainMenuScreen _mainMenuScreen;

    public void Dispose()
    {
        this.gameObject.SetActive(false);
        _onBack.onClick.RemoveAllListeners();
    }

    public void Initialize(IScreenInit hidenObj)
    {
        hidenObj.Dispose();

        this.gameObject.SetActive(true);

        _onBack.onClick.AddListener(() => _mainMenuScreen.Initialize(this));
    }
}
