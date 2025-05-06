using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNumber;

    private int number = 0;
    private Image image;
    private RectTransform rectTransform;

    public int Number
    {
        set
        {
            number = value;
            textNumber.text = number.ToString();
        }

        get => number;
    }

    public Vector3 Position => rectTransform.position;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnSelected()
    {
        image.color = Color.blue;
    }

    public void OnDeselected()
    {
        image.color = Color.red;
    }
}
