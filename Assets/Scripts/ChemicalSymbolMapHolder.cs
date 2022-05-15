using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSymbolMapHolder : MonoBehaviour
{
    [SerializeField] private ChemicalSymbolMap chemicalSymbolMap;

    public ChemicalSymbolMap ChemicalSymbolMap => chemicalSymbolMap;
}
