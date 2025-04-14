using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
   // [SerializeField] private float damage; // Damage variable removed as PlayerLife uses Die()

   [Header("Fire Trap Timers")] // Corrected attribute casing
   [SerializeField] private float activationDelay;
   [SerializeField] private float activeTime;

   private Animator anim;
   private SpriteRenderer spriteRenderer; // Corrected type casing

   private bool triggered; // Has the trap been triggered?
   private bool active; // Is the trap currently active?

   private void Awake() {
      anim = GetComponent<Animator>();
      spriteRenderer = GetComponent<SpriteRenderer>(); // Corrected type casing
   }

   // Corrected method name and simplified logic
   private void OnTriggerEnter2D(Collider2D collision)
   {
      // Use CompareTag for potentially better performance and null safety
      if (collision.CompareTag("Player") && !triggered)
      {
         // Start the activation process only if not already triggered
         StartCoroutine(ActivateFireTrap());
      }
   }

   // Added method to continuously check if player is inside the active trap
   private void OnTriggerStay2D(Collider2D collision)
   {
       if (collision.CompareTag("Player") && active)
       {
           // Get the PlayerLife component and call Die()
           PlayerLife playerLife = collision.GetComponent<PlayerLife>();
           if (playerLife != null)
           {
               playerLife.Die(); // Player dies instantly when touching active trap
           }
           else
           {
               // Log an error if the PlayerLife component is missing for some reason
               Debug.LogError("PlayerLife component not found on the object tagged 'Player'!");
           }
       }
   }

   private IEnumerator ActivateFireTrap()
   {
      // Change color to red to indicate activation
      triggered = true; // Set triggered to true
      spriteRenderer.color = Color.red; 

      // Wait for activation delay
      yield return new WaitForSeconds(activationDelay); 
      spriteRenderer.color = Color.white; // Reset color to normal
      active = true; // Set active to true
      anim.SetBool("activated", true); // Ensure correct casing

      // Wait for active time
      yield return new WaitForSeconds(activeTime); 
      active = false; // Set active to false
      triggered = false; // Reset triggered to false
      anim.SetBool("activated", false); // Ensure correct casing
   }
}
