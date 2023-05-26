using TMPro;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    [SerializeField] private TMP_Text _info;

    public void SetDescription(string description)
    {
        gameObject.SetActive(true);
        _info.text = description;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
