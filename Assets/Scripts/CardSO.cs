using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card Info", fileName = "New Card")]
public class CardSO : ScriptableObject
{
    public Image cardImage;
    public string cardText;
}
