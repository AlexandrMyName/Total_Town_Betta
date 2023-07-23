using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class ChangeLocale : MonoBehaviour, IChangeLocalization
{
    [SerializeField] private TMP_Text _tmpText;

    [SerializeField] private string localizationWorld_KeyID;


    
    public void Change()
    {
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

 
