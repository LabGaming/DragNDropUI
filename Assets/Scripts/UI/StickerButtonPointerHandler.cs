using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StickerButtonPointerHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private StickerButton _stickerButton;

    void Awake()
    {
        _stickerButton = GetComponentInParent<StickerButton>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        _stickerButton.OnPointerDown(data);
    }

    public void OnPointerUp(PointerEventData data)
    {
        _stickerButton.OnPointerUp(data);
    }

    public void OnDrag(PointerEventData data)
    {
        _stickerButton.OnDrag(data);
    }
}
