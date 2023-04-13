using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Add this line to use the new Input System

public class TimeController : MonoBehaviour
{
    public float slowTimeScale = 0.2f;
    public float normalTimeScale = 1f;
    public float slowDuration = 3f;

    private PlayerInput playerInput;  // Add this line to reference the new Input System actions

    public bool IsTimeSlowed { get; private set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindActionMap("Player").FindAction("SlowTime").performed += ctx => StartCoroutine(SlowTime()); // Bind the SlowTime action to the SlowTime method
    }

    private void OnEnable()
    {
        playerInput.actions.FindActionMap("Player").Enable(); // Enable the new Input System actions
    }

    private void OnDisable()
    {
        playerInput.actions.FindActionMap("Player").Disable(); // Disable the new Input System actions
    }

   public IEnumerator SlowTime()
    {
        if (IsTimeSlowed) yield break;

        IsTimeSlowed = true;
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(slowDuration);

        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        IsTimeSlowed = false;
    }
}
