using UnityEngine;
using UnityEngine.EventSystems;

public class PopupDimmerClose : MonoBehaviour, IPointerClickHandler
{
    public UIManager ui; // drag UIManager

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ui != null) ui.ClosePopup();
    }
}
