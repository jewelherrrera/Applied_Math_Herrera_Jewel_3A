using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool isMoving;
    private Transform goalTarget;

    private void Start()
    {
        goalTarget = GameObject.FindGameObjectWithTag("Goal").transform;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
        
    }

    public void MoveToTarget()
    {
        Vector3 direction = goalTarget.position - transform.position;

        // Ignore Y movement
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            direction.Normalize();

            // Move
            transform.position += direction * speed * Time.deltaTime;

            // Rotate toward target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }
    }
    public void OnDied()
    {
        Destroy(gameObject);
        GameManager.Instance.AddScore(100);
        GameManager.Instance.SaveHighScore();
    }

    public void OnReachGoal()
    {      
        GameManager.Instance.health--;
        GameManager.Instance.killedEnemyCount++;
        GameManager.Instance.uimanager.SetHealthLabel();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
           OnReachGoal();
        }
    }
}
