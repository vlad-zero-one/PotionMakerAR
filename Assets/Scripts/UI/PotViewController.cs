using System.Linq;
using UnityEngine;

public class PotViewController : MonoBehaviour
{
    [SerializeField] private PotSpritesMap spritesMap;

    private SpriteRenderer spriteRenderer;
    private Pot pot;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pot = GetComponent<Pot>();
        pot.OnStatusChanged += ChangeSprite;
    }

    private void ChangeSprite(PotionStatus newStatus)
    {
        var mapElement = spritesMap.Value.FirstOrDefault(x => x.Status == newStatus);
        if (mapElement != null)
        {
            spriteRenderer.sprite = mapElement.Sprite;
        }
    }

    private void OnDestroy()
    {
        pot.OnStatusChanged -= ChangeSprite;
    }
}
