using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();

        Instance = this;
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax){
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        bool plateMatchesRecipe = false;
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO recipe = waitingRecipeSOList[i];
            plateMatchesRecipe = CheckRecipeMatchesPlate(recipe, plateKitchenObject);

            if (plateMatchesRecipe)
            {
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        OnRecipeFailed.Invoke(this, EventArgs.Empty);
    }

    private bool CheckRecipeMatchesPlate(RecipeSO waitingRecipe, PlateKitchenObject plateKitchenObject)
    {
        if (waitingRecipe.KitchenObjectSoList.Count != plateKitchenObject.GetKitchenObjectSOList().Count)
        {
            return false;
        }

        bool plateHasAllIngredients = true;

        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipe.KitchenObjectSoList)
        {
            if (!plateKitchenObject.HasIngredient(recipeKitchenObjectSO))
            {
                plateHasAllIngredients = false;
                break;
            }
        }

        return plateHasAllIngredients;
    }

    public List<RecipeSO> GetWaitingRecipes()
    {
        return waitingRecipeSOList;
    }
}
