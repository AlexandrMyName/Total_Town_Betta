using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeftUI_P : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _image;
    [Space, Header("Slider(Health) values")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Image _backGround;
    [SerializeField] private Image _fill;

    [SerializeField] private SelectableValue _selectableValue;
    private void Awake()
    {
        _selectableValue.OnValueChanged += onSelected;
     
        onSelected(null);
    }
    private void onSelected(ISelectable selectable)
    {
        
        _healthSlider.gameObject.SetActive(selectable != null);
        _image.gameObject.SetActive(selectable != null);

        if (selectable == null) return;
        _nameText.text = selectable.Name;
        _healthText.text = $"{selectable.Health}/{selectable.MaxHealth}";
        _healthSlider.maxValue = selectable.MaxHealth;
        _healthSlider.value = selectable.Health;
        _image.sprite = selectable.Icon;

        Color colorLRP = Color.Lerp(Color.red, Color.green,  selectable.Health / selectable.MaxHealth);
        
        _backGround.color = colorLRP * 0.5f;
        _fill.color = colorLRP;
    }

    private void OnDestroy()
    {
        _selectableValue.OnValueChanged -= onSelected;
    }
}
