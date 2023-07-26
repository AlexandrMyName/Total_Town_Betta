using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class ChangeLocale : MonoBehaviour, IChangeLocalization
{
    [SerializeField] private TMP_Text _tmpText;

    [SerializeField] private string localizationWorld_KeyID;

    FontsDataBase fontsData;

    int indexLang;
    private void Awake()
    {
        SetFont();



    }

    private void SetFont()
    {
        fontsData = GameObject.FindWithTag("Localizator").GetComponent<FontsDataBase>();
        if (PlayerPrefs.HasKey("Lang"))
        {
            indexLang = PlayerPrefs.GetInt("Lang");
        }
        switch (indexLang)
        {
            case 0:
                _tmpText.font = fontsData.Other;
                break;


                case 1:
                _tmpText.font = fontsData.Other;
                break;

            case 2:
                _tmpText.font = fontsData.Ru;
                break;
            case 3:
                _tmpText.font = fontsData.Other;
                break;

            case 4:

                _tmpText.font = fontsData.Chinease;
                break;

            case 5:
                _tmpText.font = fontsData.Other;
                break;

            case 6:
                _tmpText.font = fontsData.Other;
                break;

            case 7:
                _tmpText.font = fontsData.Other;
                break;

            case 8:
                _tmpText.font = fontsData.Other;
                break;
            case 9:
                _tmpText.font = fontsData.Other;
                break;
            case 10:
                _tmpText.font = fontsData.Other;
                break;
            case 11:
                _tmpText.font = fontsData.Japanese;
                break;
            case 12:
                _tmpText.font = fontsData.Korean;
                break;
            case 13:
                _tmpText.font = fontsData.Other;
                break;
            case 14:

                _tmpText.font = fontsData.Ru;
                break;
            case 15:
                _tmpText.font = fontsData.Other;
                break;
            case 16:
                _tmpText.font = fontsData.Other;
                break;
            case 18:
                _tmpText.font = fontsData.Other;
                break;
            case 19:
                _tmpText.font = fontsData.Other;
                break;
            case 20:
                _tmpText.font = fontsData.Other;
                break;
            case 21:
                _tmpText.font = fontsData.Other;
                break;
        }
    }
    public void Change()
    {

        SetFont();

        LocalizationSettings.StringDatabase.GetTableAsync(LocalizationTableNames.MenuNameTable).Completed += handle =>
        {
           if(handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
           {

                StringTable table = handle.Result;

                _tmpText.text = table.GetEntry(localizationWorld_KeyID).GetLocalizedString();
            }
           else
           {
               Debug.Log(handle.OperationException);
           }


        };
         
    }
    
}

 
