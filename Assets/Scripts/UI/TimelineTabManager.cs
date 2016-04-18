using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class TimelineTabManager : MonoBehaviour {

    public static TimelineTabManager Instance;

    private int _currentSelectedTabIndex = -1;
    private TimelineTab[] _tabs;

    // Use this for initialization
    void Start() {
        if (TimelineTabManager.Instance != null)
        {
            Destroy(TimelineTabManager.Instance.gameObject);
        }

        Instance = this;

        _tabs = GetComponentsInChildren<TimelineTab>();

        Array.Sort(_tabs, delegate (TimelineTab tab1, TimelineTab tab2) {
            return tab1.name.CompareTo(tab2.name);
        });
    }

    public void SelectTab(TimelineTab tab)
    {
        if (tab != null)
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                if (_tabs[i] == tab)
                {
                    _currentSelectedTabIndex = i;
                    break;
                }
            }

            UpdateTabsPosition();
        }
        else
        {
            _currentSelectedTabIndex = -1;
            ResetAll();
        }
    }

    public void UpdateTabsPosition()
    {
        TimelineTab currentTab = _tabs[_currentSelectedTabIndex];
        for (int i = 1; i < _tabs.Length - 1; i++)
        {
            if (_tabs[i].originalPosition.x < currentTab.originalPosition.x)
            {
                _tabs[i].transform.DOLocalMoveX(_tabs[i].originalPosition.x - (25 * i), 1f);
            }
            else if(_tabs[i].transform.localPosition.x > currentTab.originalPosition.x)
            {
                _tabs[i].transform.DOLocalMoveX(_tabs[i].originalPosition.x + 25, 1f);
            }
        }

        if (currentTab != _tabs[_tabs.Length - 1])
        {
            currentTab.transform.DOLocalMoveX(currentTab.originalPosition.x + 150, 1f);
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < _tabs.Length; i++)
        {
            _tabs[i].transform.DOLocalMoveX(_tabs[i].originalPosition.x, 1f);
            //_tabs[i].transform.localPosition = _tabs[i].originalPosition;
        }
    }

    public void UpdateStickerToCurrentTab(Sticker sticker)
    {
        if (_currentSelectedTabIndex >= 0)
        {
            _tabs[_currentSelectedTabIndex].AddSticker(sticker);
        }
        else
        {
            if (sticker.parentTab != null)
            {
                sticker.parentTab.RemoveSticker(sticker);
            }
            Destroy(sticker.gameObject);
        }
    }
}
