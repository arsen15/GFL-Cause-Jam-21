using System.Collections;
using System;
using UnityEngine;
public enum State {
    Shooting,
    Hopping
}

public class Snake : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    public bool isShooting;
    // Make this a list in case we want more than 1 item
    public GameObject[] itemDrops;

    // Enemy movement
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public float spitWarmUpTime;
    public SpriteRenderer spriteRenderer;
    public Transform playerTransform;
    // Freeze Bullet interaction
    public float freezeTime = 3f;
    private bool isFrozen = false;
    public Animator animator;
    public GameObject bullet;
    public Transform mouthFacingLeftTransform;
    public Transform mouthFacingRightTransform;
    private float hopCooldownTimer; // Make public to debug
    private float hopDurationTimer; // Make public to debug
    private GameObject player;
    public int distanceFromPlayer;
    // Start is called before the first frame update

    [SerializeField] float _InitialVelocity;
    [SerializeField] float _Angle;
    [SerializeField] LineRenderer _Line;
    [SerializeField] float _Step;
    [SerializeField] float _Height;
    //private Camera cam;

    public float hopDurationPeriod;

    public float hopCooldownPeriod;

    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;  
    }

    // Update is called once per frame
    void Update()
    {      
        hopCooldownTimer += Time.deltaTime;

        if (isFrozen) return;

        // Enemy hop and patrol logic
        if (hopCooldownTimer > hopCooldownPeriod && hopDurationTimer < hopDurationPeriod) {
            hopDurationTimer += Time.deltaTime;
            DrawHopPatrolFrame();
        } else if (hopDurationTimer > hopDurationPeriod) {
            hopCooldownTimer = 0;
            hopDurationTimer = 0;
        }
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if (distance < distanceFromPlayer && !isPlayerTooVerticallyDistantToShootAt())
        {
            if (!isShooting) {
                StartCoroutine(ShootWithDelay());
            }

        }

    }

    public void Freee()
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine());
        }
    }

    // Needs to be public because shooting the Boar makes it aggro.
    private IEnumerator FreezeCoroutine()
    {
        isFrozen = true;

        float originalSpeed = moveSpeed;
        moveSpeed = 0;

        yield return new WaitForSeconds(freezeTime);

        moveSpeed = originalSpeed;
        isFrozen = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        DamagePopUpGenerator.current.CreatePopUp(transform.position, damage, Color.red);
        if ( currentHealth <= 0 )
        {
            Die();
            ItemDrop();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void ItemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            GameObject droppedBullet = Instantiate(itemDrops[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            PlayerBullet bulletScript = droppedBullet.GetComponent<PlayerBullet>();
            if (bulletScript != null)
            {
                bulletScript.InitializeBullet(false); // Ensure the bullet won't be destroyed on collision
            }
            Debug.Log("Item dropped");
        }
    }

   
    private float QuadraticEquation(float a, float b, float c, float sign) {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePathWithHeight(Vector3 targetPos, float h, out float v0, out float _Angle, out float time) {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * _Height);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin = QuadraticEquation(a, b, c, -1);
        time = tplus > tmin ? tplus : tmin;

        _Angle = Mathf.Atan(b * time / xt);

        v0 = b / Mathf.Sin(_Angle);
    }

    // For debugging
    private void DrawPath(float v0, float _Angle, float time, float step) {
        step = Mathf.Max(0.01f, step);
        _Line.positionCount = (int)(time / step) + 2;
        int count = 0;
        for(float i = 0; i < time; i += step) {
            float x = v0 * i * Mathf.Cos(_Angle);
            float y = v0 * i * Mathf.Sin(_Angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);
            _Line.SetPosition(count, transform.position + new Vector3(x,y,0));
            count++;
        }

        float xfinal = v0 * time * Mathf.Cos(_Angle);
        float yfinal = v0 * time * Mathf.Sin(_Angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);
        _Line.SetPosition(count, transform.position + new Vector3(xfinal,yfinal,0));

    }

    private void DrawHopPatrolFrame() {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.5f)
            {
                spriteRenderer.flipX = false;
                patrolDestination = 1;
            }
        }

        else if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.5f)
            {
                spriteRenderer.flipX = true;
                patrolDestination = 0;
            }
        }
    }
    
    // This is literally graphing it...
    IEnumerator CoroutineGraphVenom(GameObject venom, float v0, float _Angle, float time) {
        float t = 0;
        while (t < time) {
                float x = v0 * t * Mathf.Cos(_Angle);
                float y = v0 * t * Mathf.Sin(_Angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
                if(venom) venom.transform.position = new Vector3(x,y,0) + (spriteRenderer.flipX ? mouthFacingLeftTransform.position: mouthFacingRightTransform.position);
                t+= Time.deltaTime;
                yield return null;
        }
        Destroy(venom);
    }

    IEnumerator ShootWithDelay() {
        animator.SetBool("isShooting", true);
        isShooting = true;
        yield return new WaitForSeconds(spitWarmUpTime);
        shoot();
        isShooting = false;
        animator.SetBool("isShooting", false);
    }
    
    void shoot()
    {
        GameObject newVenom = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y-1, 0) - transform.position; //cam.ScreenToWorldPoint(Input.mousePosition) - mouthPosition.position;
        targetPos.z = 0;
        float height = Mathf.Max(0.01f, targetPos.y + targetPos.magnitude / 2f);
        float _Angle;
        float v0;
        float time;
        CalculatePathWithHeight(targetPos, height, out v0, out _Angle, out time);
        StartCoroutine(CoroutineGraphVenom(newVenom, v0, _Angle, time));
    
    }

    private bool isPlayerTooVerticallyDistantToShootAt() {
        return Math.Abs(Math.Abs(playerTransform.position.y) - Math.Abs(transform.position.y)) > _Height;
    }




}
