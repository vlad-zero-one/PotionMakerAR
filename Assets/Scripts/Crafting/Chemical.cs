using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Chemical : MonoBehaviour
{
    [SerializeField] private ChemicalType type;
    [SerializeField] private Text chemicalSymbol;

    public ChemicalType Type => type;

    private void Awake()
    {
        var symbolMap = GetComponentInParent<ChemicalSymbolMapHolder>().ChemicalSymbolMap.Values;

        var mapElement = symbolMap.FirstOrDefault(element => element.Chemical == type);
        if (mapElement != null)
        {
            chemicalSymbol.text = mapElement.Symbol;
        }
    }
}