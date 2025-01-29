using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    public GameObject enemy;            
    public Transform firePoint;         
    public GameObject rocketPrefab;

    public float detectionRadius = 5f;   
    public float attackRange = 2f;       
    public float rotationSpeed = 5f;     

    public float fireRate = 1f;         
    private float fireCooldown = 0f;    

    private bool isEnemyInRange = false;

    void Update()
    {
        DetectEnemy();                  
        if (isEnemyInRange)
        {
            RotateTowardsEnemy();        
            if (fireCooldown <= 0)
            {
                FireAtEnemy();            
                fireCooldown = fireRate;  
            }

            if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRange)
            {
                Destroy(enemy);
                RestartScene();
            }
        }

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void DetectEnemy()
    {

        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

        if (distanceToEnemy <= detectionRadius)
        {           
            isEnemyInRange = true;
        }
        else
        {
            isEnemyInRange = false;
        }
    }


    void RotateTowardsEnemy()
    {
        // Calculate direction to the enemy
        Vector2 direction = (enemy.transform.position - transform.position).normalized;

        // calculate angle to rotate
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // rotate to face enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FireAtEnemy()
    {
        
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
        Rocket rocketScript = rocket.GetComponent<Rocket>();  

        Vector3 direction = (enemy.transform.position - firePoint.position).normalized;
        rocketScript.RocketDirection(direction);  
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
