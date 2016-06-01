using UnityEngine;
using UnityEngine.UI;

public class RollText : MonoBehaviour
{
    int currentText = 0;
    int wantedText = 0;
    public TMPro.TextMeshProUGUI target;

    public int text
    {
        get
        {
            return wantedText;
        }

        set
        {
            wantedText = value;
        }
    }

    new void Start()
    {
        currentText = wantedText;
    }

    void FixedUpdate()
    {
        if (text > currentText)
        {
            currentText++;
            target.text = currentText.ToString();
        }
        else if (text < currentText)
        {
            currentText--;
            target.text = currentText.ToString();
        }
    }
}