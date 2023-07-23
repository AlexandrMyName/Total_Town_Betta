 
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangerLocalOnAwake : MonoBehaviour
{
    [SerializeField] private LocalizationView _view;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("Lang"))
        {
            int indexLang = PlayerPrefs.GetInt("Lang");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[indexLang];


            _view.ChangeLocalization();

        }
        else
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[6];
            Debug.Log("None Localization lanquege on memory! Set default - english");
        }
    }
}
