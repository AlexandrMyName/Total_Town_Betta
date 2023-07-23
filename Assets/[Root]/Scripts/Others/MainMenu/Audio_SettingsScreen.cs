using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_SettingsScreen : MonoBehaviour , IScreenInitializer
{
    [Header("Buttons / UI")]
    
    [SerializeField] private Button _onBack;
    [SerializeField] private AudioMixer _mixerMusic;
    [SerializeField] private AudioMixer _mixerEffects;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _effects;

    [Space(10), Header("Screens")] //Views
    [SerializeField] private MainMenuSettingsScreen _settingsScreen;
   

     

    public void Dispose()
    {
        this.gameObject.SetActive(false);

       
        _onBack.onClick.RemoveAllListeners();
    }
    
    public void Initialize(IScreenInitializer hidenObj) {


        hidenObj.Dispose();
        
        if( _mixerMusic.GetFloat("Music", out var musicVolume))
        {
            _music.value = musicVolume;
        }

        this. gameObject.SetActive(true);
        _onBack.onClick.AddListener(() => _settingsScreen.Initialize(this));
        
    }

    private void Update()
    {
        _mixerMusic.SetFloat("Music", _music.value);
    }
    private void OnDestroy() => Dispose();
}
 
