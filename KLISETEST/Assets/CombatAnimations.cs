using UnityEngine;

public class CombatAnimations : MonoBehaviour
{
    private AnimationClip[] attackAnimations;
    private BoxCollider2D swordCollider;

    private void Awake()
    {
        attackAnimations = Resources.LoadAll<AnimationClip>("");
        Debug.Log("Loaded " + attackAnimations.Length + " animations"); // Add this line


        //swordCollider = transform.Find("swordHitBox").GetComponent<BoxCollider2D>();
        swordCollider = GameObject.FindGameObjectWithTag("swordHitBox").GetComponent<BoxCollider2D>();
        Debug.Log("Sword collider: " + swordCollider);

        AddAnimationEvents();
    }

    public void EnableSwordCollider()
    {
        Debug.Log("Enabling sword collider"); // Add this line
        swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        Debug.Log("Disabling sword collider"); // Add this line
        swordCollider.enabled = false;
    }

    private void AddAnimationEvents()
    {
        foreach (AnimationClip clip in attackAnimations)
        {
            // Create and add an event for enabling the collider
            AnimationEvent enableColliderEvent = new AnimationEvent
            {
                time = 0, // Set the time to the first frame
                functionName = "EnableSwordCollider"
            };
            clip.AddEvent(enableColliderEvent);

            // Create and add an event for disabling the collider
            AnimationEvent disableColliderEvent = new AnimationEvent
            {
                time = clip.length - (3 / clip.frameRate), // Set the time to the last frame
                functionName = "DisableSwordCollider"
            };
            clip.AddEvent(disableColliderEvent);
        }
    }

}
