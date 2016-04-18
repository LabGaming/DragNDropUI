using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Sticker : MonoBehaviour {

    public TimelineTab parentTab;
    public int slotIndex;

    private DragPanel _dragPanel;
    private StickerButton _parentButton;

    private bool _initialized = false;
    private PointerEventData _initialDragData = null;

    public StickerButton ParentButton
    {
        get { return _parentButton; }
    }

    void Awake()
    {
        _dragPanel = GetComponentInChildren<DragPanel>();
    }

    void Start()
    {
        _initialized = true;

        if (_initialDragData != null)
        {
            StartDrag(_initialDragData);
        }
    }

    public void SetParentButton(StickerButton parentButton)
    {
        _parentButton = parentButton;
        gameObject.GetComponent<Image>().sprite = _parentButton.stickerIcon;
    }

    public void StartDrag(PointerEventData data)
    {
        if (_initialized)
        {
            GameController.currentSticker = this;
            _dragPanel.OnPointerDown(data);
        }
        else
        {
            _initialDragData = data;
        }
    }

    public void Drag(PointerEventData data)
    {
        _dragPanel.OnDrag(data);
    }

    public void StopDrag(PointerEventData data)
    {
        _dragPanel.OnPointerUp(data);
    }
}
