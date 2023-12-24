using System;
using UnityEngine;

public abstract class UI_Popup : UI_Base
{
    protected CanvasGroup _canvasGroup;
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
        _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
    }
    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
    private Transform _tr;
    protected bool _isTransition = false;
    protected void OpenPop(Transform tr, Action action = null, bool isForced = false)
    {
        if (!isForced && _isTransition) return;

        _canvasGroup.alpha = 0;

        _isTransition = true;
        _tr = tr;
        tr.localScale = Vector3.one;
    }
    protected void ClosePop(Transform tr, Action action = null, bool isForced = false)
    {
        if (!isForced && _isTransition) return;

        _isTransition = true;
        _tr = tr;
        tr.localScale = Vector3.zero;

        _isTransition = false;
        ClosePopupUI();
    }
}