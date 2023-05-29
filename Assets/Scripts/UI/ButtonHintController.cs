using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHintController : MonoBehaviour
{
    [SerializeField] private Image _buttonIco;
    [SerializeField] private Image _itemIco;
    [SerializeField] private TMP_Text _textHint;

    public Sprite ButtonIco
    {
        get => _buttonIco.sprite;
        set => _buttonIco.sprite = value;
    }

    public Sprite ItemIco
    {
        get => _itemIco.sprite;
        set
        {
            _itemIco.sprite = value;
            _itemIco.gameObject.SetActive(true);
            _textHint.gameObject.SetActive(false);
        }
    }

    public string TextHint
    {
        get => _textHint.text;
        set
        {
            _textHint.text = value;
            _textHint.gameObject.SetActive(true);
            _itemIco.gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
