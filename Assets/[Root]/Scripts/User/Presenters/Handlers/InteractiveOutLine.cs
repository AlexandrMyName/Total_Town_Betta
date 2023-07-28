using UnityEngine;
using Zenject;

public class InteractiveOutLine : MonoBehaviour
{
    private SelectableValue _selectableValue;
    private ISelectable cahedSellectable;

    [Inject]
    private void Constract(SelectableValue selectableValue)
    {
        _selectableValue = selectableValue;
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