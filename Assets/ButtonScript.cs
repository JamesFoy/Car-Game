using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public GameObject selectionArrows;

    public GameObject trackHologram;
    public GameObject arenaHologram;

    public UnityEvent onHighlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHighlight.Invoke();

        if (selectionArrows == null)
            return;

        if (this.gameObject.name == "Track Map Button")
        {
            selectionArrows.transform.position = this.transform.position;
            trackHologram.SetActive(true);
            arenaHologram.SetActive(false);
        }
        else if (this.gameObject.name == "Arena Map Button")
        {
            selectionArrows.transform.position = this.transform.position;
            trackHologram.SetActive(false);
            arenaHologram.SetActive(true);
        }
        else if (this.gameObject.name == "Simple Track Button")
        {
            selectionArrows.transform.position = this.transform.position;
            trackHologram.SetActive(false);
            arenaHologram.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {

    }
}
