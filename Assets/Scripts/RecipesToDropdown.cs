using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RecipesToDropdown : MonoBehaviour
{
    [SerializeField] Recipes recipesData;
    [SerializeField] ChemicalSymbolMap map;

    void Awake()
    {
        var dropdown = GetComponent<Dropdown>();

        var dict = map.Values.ToDictionary(elem => elem.Chemical, elem => elem.Symbol);

        var listForDropdown = new List<string>();

        foreach (var recipe in recipesData.Values)
        {
            var recipeContainment = $"{recipe.Name}(";

            foreach (var chem in recipe.Chemicals)
            {
                recipeContainment += $"{dict[chem]}+";
            }

            if (recipe.Processing == ProcessingType.Cooling)
            {
                recipeContainment += "Cool";
            }
            else if (recipe.Processing == ProcessingType.Heating)
            {
                recipeContainment += "Heat";
            }
            else
            {
                recipeContainment = recipeContainment.TrimEnd('+');
            }

            recipeContainment = $"{recipeContainment})";

            listForDropdown.Add(recipeContainment);
        }


        dropdown.AddOptions(listForDropdown);
    }
}
