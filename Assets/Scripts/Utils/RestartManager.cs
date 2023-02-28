using UnityEngine;

public class RestartManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private ScoreContainer scoreContainer;
    [SerializeField] private Pot pot;
    [SerializeField] private RecipeRequest recipeRequest;

    public void Restart()
    {
        timer.Restart();
        scoreContainer.Reset();
        pot.ClearPot();
        recipeRequest.ChangeRecipe();
    }
}
