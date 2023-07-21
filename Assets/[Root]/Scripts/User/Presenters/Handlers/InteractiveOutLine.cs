using UnityEngine;

public class InteractiveOutLine : MonoBehaviour
{
    [SerializeField] private SelectableValue _selectableValue;
    private ISelectable cahedSellectable;

    private void Awake()
    {
        _selectableValue.OnValueChanged += onSelected;
        onSelected(null);
    }
    private void onSelected(ISelectable selectable)
    {
        if(selectable == cahedSellectable) return;

        if(cahedSellectable != null)
        cahedSellectable.PointerOfTransform.GetComponent<Outline>().enabled = false;

        cahedSellectable = selectable;

        if (selectable == null) return;
        Outline outLine =  selectable.PointerOfTransform.gameObject.GetComponent<Outline>();
        if(outLine != null)
        outLine.enabled = true;
    
    }

    private void OnDestroy()
    {
        _selectableValue.OnValueChanged -= onSelected;
    }
}