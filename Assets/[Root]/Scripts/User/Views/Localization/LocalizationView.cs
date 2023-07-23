
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationView : MonoBehaviour, IScreenInitializer
{
   
    [SerializeField] private Button _en;
    [SerializeField] private Button _ru;
    [SerializeField] private Button _af;
    [SerializeField] private Button _ar;
    [SerializeField] private Button _hy;
    [SerializeField] private Button _be;
    [SerializeField] private Button _bg;
    [SerializeField] private Button _zh_Hant;
    [SerializeField] private Button _da;
    [SerializeField] private Button _fr;
    [SerializeField] private Button _de;
    [SerializeField] private Button _ga;
    [SerializeField] private Button _it;
    [SerializeField] private Button _ja;
    [SerializeField] private Button _ko;
    [SerializeField] private Button _pl;
    [SerializeField] private Button _es;
    [SerializeField] private Button _tr;
    [SerializeField] private Button _uk;

     
    [SerializeField] private List<ChangeLocale> _allChangeble; 
    public void Awake()
    {
        _af.onClick.AddListener(() => ChangeLocale(0));

        _ar.onClick.AddListener (() => ChangeLocale(1));

      

        _be.onClick.AddListener(() => ChangeLocale(2));

        _bg.onClick.AddListener(() => ChangeLocale(3));

        _zh_Hant.onClick.AddListener(() => ChangeLocale(4));

        _da.onClick.AddListener(() => ChangeLocale(5));

        _en.onClick.AddListener(() => ChangeLocale(6));

        _fr.onClick.AddListener(() => ChangeLocale(7));

        _de.onClick.AddListener(() => ChangeLocale(9));

      

        _it.onClick.AddListener(() => ChangeLocale(10));

        _ja.onClick.AddListener(() => ChangeLocale(11));

        _ko.onClick.AddListener(() => ChangeLocale(12));

        _pl.onClick.AddListener(() => ChangeLocale(13));

        _ru.onClick.AddListener(() => ChangeLocale(14));

        _es.onClick.AddListener(() => ChangeLocale(15));

        _tr.onClick.AddListener(() => ChangeLocale(16));

        _uk.onClick.AddListener(() => ChangeLocale(17));

       
    }

    public void Dispose()
    {
        this.gameObject.SetActive(false);
    }

    public void Initialize(IScreenInitializer hidenObj)
    {
        hidenObj.Dispose();

        this.gameObject.SetActive(true);

    }

    private void ChangeLocale(int indexLang)
    {

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[indexLang];



        PlayerPrefs.SetInt("Lang" ,indexLang);

      

        Debug.Log(LocalizationSettings.SelectedLocale);

        ChangeLocalization();
    }

    public void ChangeLocalization()
    {
        foreach (var locale in _allChangeble)
        {
            locale.Change();
        }
    }
}


public static class LocalizationTableNames
{
    public static string MenuNameTable = "MenuTable";
}