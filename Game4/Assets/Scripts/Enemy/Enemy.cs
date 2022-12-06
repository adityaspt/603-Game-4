using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public playerController playerControllerRef;

    [SerializeField]
    bool playerDetected;

    [SerializeField]
    bool isPlayerTouching = false;

    [SerializeField]
    string playerTag = "Player";

    [SerializeField]
    float moveSpeed=5;


    //public Vector2 DirectionToTarget => PlayerTransform.transform.position - detectorOrigin.position;

    //[Header("OverlapCircle Parameters")]

    //[SerializeField]
    //private Transform detectorOrigin;
    //public Vector2 detectorSize = Vector2.one;
    //public float detectorRadius=5;
    //public Vector2 detectorOriginOffset = Vector2.zero;

    //public float detectionDelay = 0.3f;

    //public LayerMask detectorLayerMask;

    //[Header("Gizmo Parameters")]
    //public Color gizmoIdleColor = Color.green;
    //public Color gizmoDetectedColor = Color.red;
    //public bool showGizmos = true;


    private void Awake()
    {
       
      
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DetectionCoroutine());
        playerControllerRef = GameObject.FindObjectOfType<playerController>();
    }

    //IEnumerator DetectionCoroutine()
    //{
    //    yield return new WaitForSeconds(detectionDelay);
    //    PerformDetection();
    //    StartCoroutine(DetectionCoroutine());
    //}

    //public void PerformDetection()
    //{
    //    Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position + detectorOriginOffset, detectorRadius, 0, detectorLayerMask);

    //    if (collider != null)
    //    {
    //        PlayerTransform = collider.gameObject.transform;
    //    }
    //    else
    //    {
    //        PlayerTransform = null;
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    if(showGizmos && detectorOrigin != null)
    //    {
    //        Gizmos.color = gizmoIdleColor;
    //        if (playerDetected)
    //        {
    //            Gizmos.color = gizmoDetectedColor;

    //        }
    //        Gizmos.DrawSphere((Vector2)detectorOrigin.position + detectorOriginOffset, detectorRadius);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if(playerDetected && !isPlayerTouching)
        MoveTowardsPlayer();

        if (isPlayerTouching)
        {
            if (timePlayerTouchingDamage >= deltaTimePlayerTouchingDamage)
            { 
                deltaTimePlayerTouchingDamage += Time.deltaTime;
            }
            else
            {
                //reset the delta time and destroy the enemy
                deltaTimePlayerTouchingDamage = 0f;
                DamagePlayer();
                Destroy(this.gameObject);
            }
        }
    }
    float timePlayerTouchingDamage = 2f;
    float deltaTimePlayerTouchingDamage = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            playerDetected = true;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            playerDetected = false;
           
        }
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(transform.position, playerControllerRef.transform.position);
        Vector2 direction = playerControllerRef.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, playerControllerRef.transform.position, moveSpeed * Time.deltaTime);

        if (direction.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            isPlayerTouching = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            isPlayerTouching = false;

        }
    }

    /// <summary>
    /// Makes player lose resources every 2 seconds if they have more than zero
    /// </summary>
    void DamagePlayer()
    {
        print("Damage Player");
        ResourceManager.resourceManagerInstance.SelectToReduceResourceOnEnemyAttack();
        ResourceManager.resourceManagerInstance.SelectToReduceResourceOnEnemyAttack();
    }
}
