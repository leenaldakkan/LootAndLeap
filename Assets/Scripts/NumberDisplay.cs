
using UnityEngine;
using UnityEngine.UI;

public class NumberDisplay : MonoBehaviour
{
    [Header("Digit Sprites")]
    [SerializeField] private Sprite[] digitSprites = new Sprite[10];

    [Header("Four Digit Images")]
    [SerializeField] private Image[] digitImages = new Image[4];

    public void SetNumber(int number)
    {
        number = Mathf.Clamp(number, 0, 9999);

        string digits = number.ToString("D4");

        for (int i = 0; i < 4; i++)
        {
            int digit = digits[i] - '0';
            digitImages[i].sprite = digitSprites[digit];
        }
    }
}

