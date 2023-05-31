using UnityEngine;

public class SubWindowController : WindowController
{
    [SerializeField] private WindowController _parrentWindow;

    public override void Hide()
    {
        _parrentWindow.Show();
        base.Hide();
    }

    public override void Show()
    {
        _parrentWindow.Hide();
        base.Show();
    }
}