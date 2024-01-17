using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public event EventHandler OnStateChange;

 
   public enum State
   {
      WaitingToStart,
      CountdownToStart,
      GamePlaying,
      GameOver
   }

   public static GameManager Instance { get; private set; }
   private State state;
   private float waitingToStartTimer = .1f;
   private float countdownToStartTimer = 5f;
   private float gamePlayingTimerMax = 30f;
   private float currentGamePlayingTimer;
   private int recipesDelivered = 0;
   

   private void Awake()
   {
      state = State.WaitingToStart;
      OnStateChange?.Invoke(this, EventArgs.Empty);
      Instance = this;
   }

   private void Start()
   {
      DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
   }

   private void DeliveryManagerOnRecipeSuccess(object sender, EventArgs e)
   {
      recipesDelivered++;
   }

   private void Update()
   {
      switch (state)
      {
         case State.WaitingToStart:
            currentGamePlayingTimer = gamePlayingTimerMax;
            waitingToStartTimer -= Time.deltaTime;
            if (waitingToStartTimer <= 0f)
            {
               state = State.CountdownToStart;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.CountdownToStart:
            countdownToStartTimer -= Time.deltaTime;
            if (countdownToStartTimer <= 0)
            {
               state = State.GamePlaying;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.GamePlaying:
            currentGamePlayingTimer -= Time.deltaTime;
            if (currentGamePlayingTimer <= 0f)
            {
               state = State.GameOver;
               OnStateChange?.Invoke(this, EventArgs.Empty);
            }

            break;
      }
      Debug.Log($"CurrentState: {state}");
   }

   public bool IsGamePlaying()
   {
      return state == State.GamePlaying;
   }

   public State GetState()
   {
      return state;
   }

   public float GetCountdownToStartTimer()
   {
      return countdownToStartTimer;
   }

   public int GetRecipesDelivered()
   {
      return recipesDelivered;
   }

   public float GetGameTimeRemainingNormalized()
   {
      return 1 - (currentGamePlayingTimer / gamePlayingTimerMax);
   }
}
