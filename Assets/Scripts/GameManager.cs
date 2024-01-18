using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public event EventHandler OnStateChange;
   public event EventHandler OnGamePaused;
   public event EventHandler OnGameUnpaused;

 
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
   private float gamePlayingTimerMax = 120f;
   private float currentGamePlayingTimer;
   private int recipesDelivered = 0;
   private bool isGamePaused = false;
   

   private void Awake()
   {
      state = State.WaitingToStart;
      OnStateChange?.Invoke(this, EventArgs.Empty);
      Instance = this;
   }

   private void Start()
   {
      DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
      GameInput.Instance.OnPauseAction += GameInputOnPauseAction;
   }

   private void GameInputOnPauseAction(object sender, EventArgs e)
   {
      TogglePauseGame();
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

   public void TogglePauseGame()
   {
      isGamePaused = !isGamePaused;
      if (isGamePaused)
      {
         Time.timeScale = 0f;
         OnGamePaused?.Invoke(this, EventArgs.Empty);
      }
      else
      {
         Time.timeScale = 1f;
         OnGameUnpaused?.Invoke(this, EventArgs.Empty);
      }
   }
}
