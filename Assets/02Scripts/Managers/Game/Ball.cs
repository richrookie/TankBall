using UnityEngine;
using TMPro;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private int _number = 1;
    public int Number
    {
        get => _number;
        private set { _number = value; }
    }

    private TextMeshProUGUI _ballText = null;
    public GameObject BallText
    {
        get => _ballText.gameObject;
    }


    public void Init(TextMeshProUGUI text)
    {
        _number = 1;
        _ballText = text;
        _ballText.text = Number.ToString();
    }

    public void IncreaseNumber(int value)
    {
        Number = value;
        _ballText.text = Number.ToString();
    }
}
