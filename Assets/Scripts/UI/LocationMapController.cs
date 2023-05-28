using UnityEngine;
using UnityEngine.UI;

public class LocationMapController : MonoBehaviour
{
    [SerializeField] private Image _content;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _viewPortTransform;
    
    public void OpenMap(Sprite sprite)
    {
        _content.sprite = sprite;

        var spriteWidth = sprite.bounds.size.x;
        var spriteHeight = sprite.bounds.size.y;

        var newImageSize = new Vector2(300  , (300 * spriteHeight) / spriteWidth);
        (_content.GetComponent<RectTransform>()).sizeDelta = newImageSize;
        
        if(spriteWidth > spriteHeight)
        {
            _rectTransform.sizeDelta = newImageSize;
            _viewPortTransform.sizeDelta = newImageSize;
            _viewPortTransform.anchoredPosition = new Vector2(_viewPortTransform.anchoredPosition.x , newImageSize.y / 2);
        }
        else 
        {
            _rectTransform.sizeDelta = new Vector2(300, 216);
            _viewPortTransform.sizeDelta = new Vector2(300, 216);
            _viewPortTransform.anchoredPosition = new Vector2(_viewPortTransform.anchoredPosition.x , 108F);
        }

        gameObject.SetActive(true);
    }

    public void HideMap()
    {
        gameObject.SetActive(false);
    }
}
