using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class Pot : MonoBehaviour
{
    private readonly float cookingTime = 2f;

    [SerializeField] private Recipes recipesData;
    [SerializeField] private Text elementsInside;

    public Recipe currentMixture;

    public PotionStatus Status = PotionStatus.Empty;
    public delegate void StatusChangeDelegate(PotionStatus newStatus);
    public event StatusChangeDelegate OnStatusChanged;

    private List<Recipe> recipes;

    private Coroutine currentProcess;

    private void Awake()
    {
        recipes = recipesData.Values.ToList();

        currentMixture = new Recipe();
    }

    private void ChangeStatus(PotionStatus newStatus)
    {
        Status = newStatus;
        OnStatusChanged?.Invoke(newStatus);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (Status == PotionStatus.Ready) return;

        var element = collider.GetComponent<Element>();
        if (element != null)
        { 
            CheckChemicals(element);
        }

        var crystall = collider.GetComponent<Crystall>();
        if (crystall != null)
        {
            CheckCrystall(crystall);
        }

        Debug.Log(ValidMixture());
    }

    private void CheckCrystall(Crystall crystall)
    {
        switch (Status)
        {
            case PotionStatus.NotEmpty:
                if (crystall.Type == ProcessingType.Cooling)
                {
                    if (currentProcess != null) StopCoroutine(currentProcess);
                    currentProcess = StartCoroutine(DelayedStatusChange(PotionStatus.Cooled));
                }
                else if (crystall.Type == ProcessingType.Heating)
                {
                    if (currentProcess != null) StopCoroutine(currentProcess);
                    currentProcess = StartCoroutine(DelayedStatusChange(PotionStatus.Heated));
                }
                break;
            case PotionStatus.Heated:
                if (crystall.Type == ProcessingType.Cooling)
                {
                    if (currentProcess != null) StopCoroutine(currentProcess);
                    currentProcess = StartCoroutine(DelayedStatusChange(PotionStatus.Bad));
                }
                break;
            case PotionStatus.Cooled:
                if (crystall.Type == ProcessingType.Heating)
                {
                    if (currentProcess != null) StopCoroutine(currentProcess);
                    currentProcess = StartCoroutine(DelayedStatusChange(PotionStatus.Bad));
                }
                break;
            case PotionStatus.Ready:
            case PotionStatus.Empty:
            case PotionStatus.Bad:
                break;
        }
    }

    private void CheckChemicals(Element element)
    {
        var type = element.Type;

        ChangeStatus(PotionStatus.NotEmpty);

        elementsInside.text = elementsInside.text + (elementsInside.text == "" ? "" : "\n") + type;

        currentMixture.Chemicals.Add(type);
    }

    private IEnumerator DelayedStatusChange(PotionStatus newStatus)
    {
        Debug.Log("STARTED COROUTINE");

        yield return new WaitForSeconds(cookingTime);

        Debug.Log("CONTINUE COROUTINE");

        ChangeStatus(newStatus);
        currentProcess = null;

        if (newStatus == PotionStatus.Cooled)
            currentMixture.Processing = ProcessingType.Cooling;
        else if (newStatus == PotionStatus.Heated)
            currentMixture.Processing = ProcessingType.Heating;

        Debug.Log(ValidMixture());
    }

    private bool ValidMixture()
    {
        var validRecipes = recipes
            .Where(recipe => recipe.Contains(currentMixture))
            .ToList();

        foreach (var recipe in validRecipes)
        {
            Debug.Log(recipe.Name);

            if (recipe == currentMixture) ChangeStatus(PotionStatus.Ready);
        }

        if (validRecipes.Count == 0) ChangeStatus(PotionStatus.Bad);

        return validRecipes.Count != 0;
    }

    public void ClearPot()
    {
        foreach (var chem in currentMixture.Chemicals)
            Debug.Log("Poured out " + chem);

        ChangeStatus(PotionStatus.Empty);

        elementsInside.text = "";

        currentMixture = new Recipe();
    }

    private void OnDestroy()
    {
        OnStatusChanged = null;
    }
}
