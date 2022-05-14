using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeRequest : MonoBehaviour
{
    [SerializeField] private Recipes recipes;
    [SerializeField] private Text recipeName;
    [SerializeField] private int requestTime = 10;

    public Recipe CurrentRequest;

    private Coroutine coroutine;
    private int currentTime;

    private void Awake()
    {
        currentTime = requestTime;

        ChangeRecipe();
    }

    private void SelectRandomRecipe()
    {
        var index = Random.Range(0, recipes.Values.Length);

        CurrentRequest = recipes.Values[index];

        recipeName.text = CurrentRequest.Name;
    }

    private IEnumerator RequestTimer(int decrement = 1)
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);

            currentTime -= decrement;
        }

        ChangeRecipe();
    }

    public void ChangeRecipe()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        currentTime = requestTime;
        SelectRandomRecipe();

        coroutine = StartCoroutine(RequestTimer());
    }
}
