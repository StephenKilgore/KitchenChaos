using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;

    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State currentState;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private float burningTimer;

    private void Start()
    {
        currentState = State.Idle;
        
    }
    
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroyKitchenObject();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        currentState = State.Fried;
                        burningTimer = 0f;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = currentState
                        } );
                    }

                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroyKitchenObject();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        currentState = State.Burned;
                        
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = currentState
                        } );
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }   
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    currentState = State.Frying;
                    fryingTimer = 0f;
                    
                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = currentState
                    } );
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
            }
            else
            {
                //player not carrying anything
            }
        } 
        else
        {
            //there is a kitchen object here
            if (!player.HasKitchenObject())
            {
                //player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                currentState = State.Idle;
                burningTimer = 0f;
                fryingTimer = 0f;
                
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = currentState
                } );
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            {
                //player carrying somehting
            }
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO currentKitchenObjectSO)
    {
        FryingRecipeSO fryingingRecipeSO = GetFryingRecipeSOWithInput(currentKitchenObjectSO);

        return fryingingRecipeSO != null ? fryingingRecipeSO.output : null;
    }
    private bool HasRecipeWithInput(KitchenObjectSO currentKitchenObjectSO)
    {
        return GetFryingRecipeSOWithInput(currentKitchenObjectSO) != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO kitchenObjectSo)
    {
        foreach (var fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == kitchenObjectSo)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO kitchenObjectSo)
    {
        foreach (var burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == kitchenObjectSo)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
