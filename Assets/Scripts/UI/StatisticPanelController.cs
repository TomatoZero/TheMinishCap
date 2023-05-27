using UnityEngine;

public class StatisticPanelController : MonoBehaviour
{
    [SerializeField] private IventoryItem _earthElement;
    [SerializeField] private IventoryItem _waterElement;
    [SerializeField] private IventoryItem _fireElement;
    [SerializeField] private IventoryItem _windElement;
    [Space]
    [SerializeField] private IventoryItem _gripRing;
    [SerializeField] private IventoryItem _powerBracelet;
    [SerializeField] private IventoryItem _flippers;
    [Space]
    [SerializeField] private SubWindow _tigerScrolls;

    
    public void OpenSubWindow(SubWindow subWindow)
    {
        subWindow.Show();
        gameObject.SetActive(false);
    }

    public void HideSubWindow(SubWindow subWindow)
    {
        subWindow.Hide();
        gameObject.SetActive(true);
    }
}
