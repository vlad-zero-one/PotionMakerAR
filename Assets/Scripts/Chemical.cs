using UnityEngine;

public class Chemical : MonoBehaviour
{
    [SerializeField] private ChemicalType type;

    public ChemicalType Type => type;
}