using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private string _windowName;

    public string WindowName => _windowName;

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
