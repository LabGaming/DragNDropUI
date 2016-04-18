using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StickerButton : MonoBehaviour
{
    public GameObject stickerPrefab;
    public Image stickerIconHolder;
    private Sticker _currentStickerChild;
    private Toggle[] _toggles;
    public Sprite stickerIcon { get; private set; }


    // Use this for initialization
    void Start() {
        _toggles = gameObject.GetComponentsInChildren<Toggle>();
        stickerIcon = stickerIconHolder.sprite;
    }

    public void OnPointerDown(PointerEventData data)
    {
        SetToggles(true);
        GameObject stickerGO = Instantiate(stickerPrefab);
        stickerGO.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
        stickerGO.transform.position = gameObject.transform.position;

        _currentStickerChild = stickerGO.GetComponent<Sticker>();

        _currentStickerChild.SetParentButton(this);
        _currentStickerChild.StartDrag(data);
    }

    public void OnPointerUp(PointerEventData data)
    {
        _currentStickerChild.StopDrag(data);
        SetToggles(false);
    }

    public void OnDrag(PointerEventData data)
    {
        _currentStickerChild.Drag(data);
    }

    void SetToggles(bool state)
    {
        foreach (Toggle toggle in _toggles)
        {
            toggle.isOn = state;
        }
    }
}
