//Authors: Maciej Dowbor, Dane Oddy
//Module: MED5192 & MED5201
//Last Accessed: 27/07/2020

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
//---Script Summary--\\
//Holds all enemy stats in this script to make balancing easier for our designer, damage enemies, check for enemy death, activate SS effect, change generator temperature upon enemy death and collisions with player projectiles
//

//*Everything that is not labeled \\Dane is done by Maciek*
public class EnemyStats : MonoBehaviour
{
    //Script references
    private SpawnerV2 spawnerScript;
    private TemperatureGauge temperatureGaugeScript;
    private WaveHandler waveHandlerScript;
    private Weapon weaponScript;
    private EnemyMovement enemyMovementScript;    

    //Script variables
    [Header("Basics")]
    [Tooltip("0 = EnemySmall\n1 = EnemyMedium\n2 = EnemyLarge")]
    public int enemyID;
    public float health;
    public float speed;
    [Tooltip("Distance at which the enemy will start to move towards the player")]
    public float triggerDistance;
    [HideInInspector]
    public float maxHealth;

    [Header("Shooting variables")]
    public int ammunition;
    [Tooltip("Projectiles per second")]
    public float rateOfFire;
    public float rangedAttackRange;
    public int rangedDamage;
    [Tooltip("Chance to hit in % e.g. 25 = 25% (Ranged Only)")]
    public int chanceToHit;

    [Header("Melee variables")]
    public int meleeDamage;
    public float meleeAttackRange;
    [Tooltip("Attacks per second")]
    public float meleeAttackRate;
    [Tooltip("Damage the Enemy takes when colliding with player")]
    public float collisionWithPlayerDamage;

    [Header("Fire StarStone variables (ID=1)")]
    [Header("StarStone effects variables")]

    public float damageOverTime;
    public float damageOverTimeInterval;
    public int ticksOfDamageOverTime;

    [Header("Ice StarStone variables (ID=2)")]
    [Tooltip("e.g. 0.2 = 20% slow")]
    public float slowPercentage;
    public float slowDuration;

    [Header("Poison StarStone variables (ID=3)")]
    public GameObject poisonPuddleObject;
    public float poisonDropInterval;
    public float poisonDespawnTime;
    public float poisonDamageOverTime;
    public float poisonDamageOverTimeInterval;
    public int poisonTicksOfDamageOverTime;
    public float poisonSlowPercentage;
    public float poisonSlowDuration;
    private Vector3 poisonPuddleSpawnOrigin;

    [Header("Electric StarStone variables (ID=4)")]
    public float stunDuration;

    [Header("other")]
    [Tooltip("Adjusts y coordinate to shoot rays at players height(Temporary fix until enemies will have weapons that will rotate towards the player)")]
    public float yRayOffset;
    private float enemyTemp;
    private int starStoneID;
    private float enemySizeY;
    private MeshRenderer enemyMeshRenderer;
    private string sceneName;


    //Dane
    [Header("StarStone Debuff Variables")]
    public GameObject fireEffect;
    public float timeOnFire;
    [Tooltip("Damage by fire per second")]
    public float fireDamage;
    public bool onFire = false;

    public GameObject iceEffect;
    public float timeFrozen;
    public float defaultSpeed;//must equal normal speed for this enemy type
    [Tooltip("Half of default")]
    public float frozenSpeed;//speed when under effect of ice star stone
    public bool frozen = false;

    public GameObject poisonEffect;
    public float timePoisoned;
    [Tooltip("Multiplies damage by this amount")]
    public float poisonedDamage;//damage is multipled by this amount when poisoned
    public bool poisoned = false;

    public GameObject electricEffect;
    public float timeStunned = 10;
    public bool stunned = false;
    //Dane


    private void Start()
    {
        //Script References
        spawnerScript = FindObjectOfType<SpawnerV2>();
        temperatureGaugeScript = FindObjectOfType<TemperatureGauge>();
        waveHandlerScript = FindObjectOfType<WaveHandler>();
        weaponScript = FindObjectOfType<Weapon>();
        enemyMovementScript = gameObject.GetComponent<EnemyMovement>();
        
        //Mesh Rendered
        enemyMeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
     
        //Set variables
        starStoneID = waveHandlerScript.starStoneID;
        maxHealth = health;
        frozenSpeed = defaultSpeed / 2; //Dane
        poisonedDamage = weaponScript.damage * 2; //Dane

        //Scene name
        sceneName = SceneManager.GetActiveScene().name;
        
        //enemySizeY = enemyMeshRenderer.bounds.size.y;

        switch (enemyID) //Used to adjust the spawn points of poison puddles to make sure theu are spawn below the enemy instead of inside of them
        {
            case 0:
                enemySizeY = 0.5f;
                break;

            case 1:
                enemySizeY = 1;
                break;

            case 2:
                enemySizeY = 2;
                break;               
        }
        //Call functions
        GetTemperature();

        //Activates poison puddle spawns if appropriate SS is active
        if (starStoneID == 3 && sceneName != "Tutorial")
        {
            StartCoroutine(SpawnPoisonPuddles());
        }
    }

    public void TakeDamage(float damageTaken) //Deals damage to the enemy
    {
        Debug.Log("damage dealt");
        health -= damageTaken;
        CheckHealth();

        if(poisoned == true)//Dane
        {
            health -= damageTaken * poisonedDamage;
        }
    }

    void GetTemperature() //Gets temperature value of coresponding enemy type set in the spawner
    {
        enemyTemp = spawnerScript.enemyTemperature[enemyID];
    }

    void SubtractTemperature() //Cools down the generator when enemy is killed
    {
        if(sceneName != "Tutorial")
        {
            temperatureGaugeScript.ChangeGeneratorTemperature(-enemyTemp);
        } 
    }

    void CheckHealth() //Checks if the enemy is dead
    {
        if(health <= 0) 
        {
            //play death animation

            SubtractTemperature();
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnPoisonPuddles() //Spawns poison puddles for as long as the enemy is alive
    {
        while(health > 0)
        {
            yield return new WaitForSeconds(poisonDropInterval);
            poisonPuddleSpawnOrigin = new Vector3(transform.position.x, transform.position.y - enemySizeY, transform.position.z);
            Instantiate(poisonPuddleObject, poisonPuddleSpawnOrigin, Quaternion.identity);            
        }
    }

    private void OnCollisionEnter(Collision collision) //Taking damage from player projectiles (Made it in case we want the player to have actual projectiles)
    {
        //if(collision.gameObject.tag == "ObjectPlayerProjectiles")
        //{
        //
        //}
        if (collision.gameObject.tag == "Player")
        {
            TakeDamage(collisionWithPlayerDamage);
        }
    }


    //Dane
    private void OnTriggerEnter(Collider collision) 
    {
        //Dane
        if (collision.gameObject.tag == "Fire" && onFire == true)
        {
            InvokeRepeating("FireDamage", 0.1f, 1f);
        }
        else if (onFire == false)
        {
            CancelInvoke("FireDamage");
        }
    }

    //Player star stone power functions
    void FireDamage()
    {
        TakeDamage(fireDamage);
    }

    void SlowEnemy()
    {
        if (frozen == true)
        {       
            speed = frozenSpeed; //Decrease enemy speed by half
        }
        else if (frozen == false)
        {
            speed = defaultSpeed;
        }
    }

    void StunEnemy()
    {
        if (stunned == true)
        {
            enemyMovementScript.enabled = false;
        }
        else if (stunned == false)
        {
            enemyMovementScript.enabled = true;
        }
    }

    //Set enemy on fire for specified time
    public IEnumerator FireEffect()
    {
        onFire = true;    
        fireEffect.SetActive(true);

        yield return new WaitForSeconds(timeOnFire);

        onFire = false;
        fireEffect.SetActive(false);        
    }

    //Freeze enemy for specified time
    public IEnumerator IceEffect()
    {
        frozen = true;
        iceEffect.SetActive(true);
        SlowEnemy();

        yield return new WaitForSeconds(timeFrozen);

        frozen = false;
        iceEffect.SetActive(false);
        SlowEnemy();
    }

    //Poison enemy for specified time
    public IEnumerator PoisonEffect()
    {
        //Refer to TakeDamage function 
        poisoned = true;
        poisonEffect.SetActive(true);

        yield return new WaitForSeconds(timePoisoned);

        poisoned = false;
        poisonEffect.SetActive(false);
    }

    //Stun enemy for specified time
    public IEnumerator ElectricityEffect()
    {
        stunned = true;
        electricEffect.SetActive(true);
        StunEnemy();

        yield return new WaitForSeconds(timeStunned);

        stunned = false;
        electricEffect.SetActive(false);
        StunEnemy();
    }
    //Dane
}
