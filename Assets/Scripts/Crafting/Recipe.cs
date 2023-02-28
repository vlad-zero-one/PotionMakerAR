using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Recipe
{
    public string Name;
    public List<ChemicalType> Chemicals;
    public ProcessingType Processing;
    public int Score;

    public Recipe()
    {
        Name = "";
        Chemicals = new List<ChemicalType>();
        Processing = ProcessingType.None;
    }

    public void Add(ChemicalType chemical)
    {
        Chemicals.Add(chemical);
    }

    public static bool operator ==(Recipe recipe1, Recipe recipe2)
    {
        var returnValue = true;

        foreach (var chem in new HashSet<ChemicalType>(recipe1.Chemicals))
        {
            if (recipe1.Chemicals.Where(type => type == chem).Count()
                != recipe2.Chemicals.Where(type => type == chem).Count())
            {
                returnValue = false;
            }
        }

        if (recipe1.Processing != recipe2.Processing) returnValue = false;

        return returnValue;
    }

    public static bool operator !=(Recipe recipe1, Recipe recipe2)
    {
        return !(recipe1 == recipe2);
    }

    public bool Contains(Recipe recipe)
    {
        var returnValue = false;

        var union = recipe.Chemicals.Union(Chemicals);

        foreach (var chem in union)
        {
            if (Chemicals.Where(type => type == chem).Count()
                >= recipe.Chemicals.Where(type => type == chem).Count())
            {
                returnValue = true;
            }
            else
            {
                return false;
            }
        }

        if (recipe.Processing != ProcessingType.None)
        {
            if (recipe.Processing != Processing)
            {
                returnValue = false;
            }
        }

        return returnValue;
    }
}
