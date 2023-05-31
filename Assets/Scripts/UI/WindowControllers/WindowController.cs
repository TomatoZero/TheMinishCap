using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private MenuController _menuController;
    [SerializeField] private string _windowName;
    [SerializeField] private GameObject _firstSelected;

    public string WindowName => _windowName;
    public GameObject FirstSelected => _firstSelected;
    public MenuController MenuController => _menuController;

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
}
