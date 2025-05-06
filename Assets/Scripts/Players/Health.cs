using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Slider playerHealthBar;

    //public Material normalMaterial; // e.g., blue
    //public Material redMaterial;    // flashing color

    //private Renderer characterRenderer;

    void Start() {
        currentHealth = maxHealth;
        //characterRenderer = GetComponentInChildren<Renderer>();

        //if (characterRenderer != null && normalMaterial != null)
        //{
        //    characterRenderer.material = normalMaterial;
        //}
    }

    private void Update()
    {
        if (playerHealthBar)
            playerHealthBar.value = currentHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Health: {currentHealth}");

        //if (characterRenderer != null)
        //{
        StartCoroutine(FlashRed());
        //}

        if (currentHealth <= 0) {
            Die();
        }
    }

    private System.Collections.IEnumerator FlashRed()
    {
        //characterRenderer.material = redMaterial;
        yield return new WaitForSeconds(0.15f); // Flash duration
        //characterRenderer.material = normalMaterial;
    }

    private void Die() {
        Debug.Log($"{gameObject.name} died!");
        // Add death animation or disable here
    }
}
