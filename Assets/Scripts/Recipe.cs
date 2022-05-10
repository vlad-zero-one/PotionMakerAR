using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string Name;
    public List<ElementType> Chemicals;
    public ProcessingType Processing;

    public Recipe()
    {
        Name = "";
        Chemicals = new List<ElementType>();
        Processing = ProcessingType.None;
    }

    public void Add(ElementType chemical)
    {
        Chemicals.Add(chemical);
    }

    public static bool operator ==(Recipe recipe1, Recipe recipe2)
    {
        var returnValue = true;

        foreach (var element in new HashSet<ElementType>(recipe1.Chemicals))
        {
            if (recipe1.Chemicals.Where(type => type == element).Count()
                != recipe2.Chemicals.Where(type => type == element).Count())
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

        foreach (var element in union)
        {
            Debug.Log("RECIPE " + Name + ": " + element + " " + Chemicals.Where(chem => chem == element).Count());
            Debug.Log("RECIPE " + recipe.Name + ": " + element + " " + recipe.Chemicals.Where(chem => chem == element).Count());


            if (Chemicals.Where(type => type == element).Count()
                >= recipe.Chemicals.Where(type => type == element).Count())
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
