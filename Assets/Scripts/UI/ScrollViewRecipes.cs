using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScrollViewRecipes : MonoBehaviour
{
    [SerializeField] Recipes recipesData;
    [SerializeField] ChemicalSymbolMap map;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] GameObject content;
    [SerializeField] GameObject recipePrefab;

    public void ShowOrHide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void Awake()
    {
        var dict = map.Values.ToDictionary(elem => elem.Chemical, elem => elem.Symbol);

        var contentRectTransform = content.GetComponent<RectTransform>();
        var contentHeight = contentRectTransform.rect.height;

        var recipePrefabRectTransform = recipePrefab.GetComponent<RectTransform>();

        var canvasHeight = canvasRectTransform.rect.height;

        recipePrefabRectTransform.sizeDelta = new Vector2(0, canvasHeight / 8);

        var recipePrefabHeight = recipePrefabRectTransform.rect.height;

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

            var go = Instantiate(recipePrefab, contentRectTransform);

            contentHeight += recipePrefabHeight;
            contentRectTransform.sizeDelta = new Vector2(0, contentHeight);

            go.GetComponent<Text>().text = recipeContainment;
        }
    }
}