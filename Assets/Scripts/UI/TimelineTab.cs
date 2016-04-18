using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimelineTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Vector3 originalPosition { get; private set; }

    private Image[] _slots;

	// Use this for initialization
	void Awake () {
        originalPosition = this.gameObject.transform.localPosition;
        _slots = gameObject.GetComponentsInChildren<Image>();
	}

    public void OnPointerEnter(PointerEventData data)
    {
        TimelineTabManager.Instance.SelectTab(this);
    }

    public void OnPointerExit(PointerEventData data)
    {
       TimelineTabManager.Instance.SelectTab(null);
    }

    public void AddSticker(Sticker sticker)
    {
        if (sticker.parentTab == this)
        {
            AnchorSticker(sticker, sticker.slotIndex);
            return;
        }

        int freeSlot = -1;

        for (int i = 1; i < _slots.Length; i++)
        {
            if (_slots[i].gameObject.activeSelf)
            {
                freeSlot = i;
                break;
            }
        }

        if (freeSlot == -1)
        {
            Destroy(sticker.gameObject);
        }
        else
        {
            AnchorSticker(sticker, freeSlot);
        }
    }

    private void AnchorSticker(Sticker sticker, int slot)
    {
        sticker.slotIndex = slot;
        sticker.parentTab = this;
        sticker.transform.SetParent(this.transform, false);
        sticker.transform.localScale = Vector3.one;
        sticker.transform.localPosition = _slots[slot].transform.localPosition;
        _slots[slot].gameObject.SetActive(false);

        sticker.GetComponent<CanvasGroup>().interactable = true;
        sticker.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void RemoveSticker(Sticker sticker)
    {
        _slots[sticker.slotIndex].gameObject.SetActive(true);
    }
}
