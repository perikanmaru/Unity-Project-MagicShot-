/// <summary>
/// Health.cs
/// Author: MutantGopher
/// This is a sample health script.  If you use a different script for health,
/// make sure that it is called "Health".  If it is not, you may need to edit code
/// referencing the Health component from other scripts
/// </summary>

using UnityEngine;

public class Health : MonoBehaviour
{
    public bool canDie = true;                  // Whether or not this health can die

    public float startingHealth = 100.0f;       // The amount of health to start with
    public float maxHealth = 100.0f;            // The maximum amount of health
   private float currentHealth = 0.0f;             // The current ammount of health

    public bool replaceWhenDead = false;        // Whether or not a dead replacement should be instantiated.  (Useful for breaking/shattering/exploding effects)
    public GameObject deadReplacement;          // The prefab to instantiate when this GameObject dies
    public bool makeExplosion = false;          // Whether or not an explosion prefab should be instantiated
    public GameObject explosion;                // The explosion prefab to be instantiated

    public bool isPlayer = false;               // Whether or not this health is the player
    public GameObject deathCam;                 // The camera to activate when the player dies

    private bool dead = false;                  // Used to make sure the Die() function isn't called twice
    
    //のこりHP参照用変数
    private float HPAmount = 100.0f;
    //最大HP参照用変数
    float StartHP = 0.0f;
    // Use this for initialization
    void Start()
    {
        // Initialize the currentHealth variable to the value specified by the user in startingHealth　HPの初期化処理
        currentHealth = startingHealth;
    }

    public void ChangeHealth(float amount)
    {
        // Change the health by the amount specified in the amount variable      Amountはマイナスの値が入る
        currentHealth += amount;
        // If the health runs out, then Die.
        if (currentHealth <= 0 && !dead && canDie)
            Die();

        // Make sure that the health never exceeds the maximum health
        else if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void Die()
    {
        // This GameObject is officially dead.  This is used to make sure the Die() function isn't called again
        dead = true;

        // Make death effects
        if (replaceWhenDead)
            Instantiate(deadReplacement, transform.position, transform.rotation);
        if (makeExplosion)
            Instantiate(explosion, transform.position, transform.rotation);

        if (isPlayer && deathCam != null)
            deathCam.SetActive(true);

        // Remove this GameObject from the scene
        Destroy(gameObject);
    }

    //オブジェクトの残りHPを返す
   public float GetHPAmount()
    {
        //残りHPの計算
        HPAmount = currentHealth;
        return HPAmount;
    }

    //最大HPを返すメソッド
    public float MaxHP()
    {

        StartHP = startingHealth;
        return StartHP;
    }
}
       


