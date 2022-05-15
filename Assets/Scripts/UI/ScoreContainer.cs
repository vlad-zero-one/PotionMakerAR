using UnityEngine;
using UnityEngine.UI;

public class ScoreContainer : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Pot pot;
    [SerializeField] private RecipeRequest recipeRequest;

    private void Start()
    {
        Reset();

        pot.OnPotionSold += RecipeSoldHandler;
    }

    private void RecipeSoldHandler(Recipe recipe)
    {
        if (recipeRequest.CurrentRequest == recipe)
        {
            AddToScore(recipe.Score * 2);
            recipeRequest.ChangeRecipe();
        }
        else
        {
            AddToScore(recipe.Score);
            recipeRequest.ChangeRecipe();
        }
    }

    public void SetScore(int value)
    {
        score.text = value.ToString();
    }

    public void AddToScore(int value)
    {
        if (int.TryParse(score.text, out int currentScore))
        {
            currentScore += value;

            SetScore(currentScore);
        }
        else
        {
            Reset();
        }
    }

    public void Reset()
    {
        SetScore(0);
    }

    private void OnDestroy()
    {
        pot.OnPotionSold -= RecipeSoldHandler;
    }
}
