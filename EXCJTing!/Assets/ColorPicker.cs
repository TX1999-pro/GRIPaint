using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Button[] colorButtons;
    public Image whiteImage;
    public static Color SelectedColor { get; private set; } = Color.black;

    private void Start()
    {
        foreach (Button button in colorButtons)
        {
            button.onClick.AddListener(() => SelectCurrentColor(button.image.color));
        }
    }

    public void SelectCurrentColor(Color color)
    {
        SelectedColor = color;
        UpdateWhiteImageColor();
    }

    private void UpdateWhiteImageColor()
    {
        whiteImage.color = SelectedColor;
    }
}