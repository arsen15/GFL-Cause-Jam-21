using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpitter : MonoBehaviour
{
    public GameObject bullet;
    public GameObject mouth;

    private float timer;
    private GameObject player;
    public int distanceFromPlayer;

    public float shootingPeriod;
    // Start is called before the first frame update

    [SerializeField] float _InitialVelocity;
    [SerializeField] float _Angle;
    [SerializeField] LineRenderer _Line;
    [SerializeField] float _Step;
    [SerializeField] float _Height;
    //private Camera cam;


    void Start()
    {
        //cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if (distance < distanceFromPlayer)
        {
            timer += Time.deltaTime;

            if (timer > shootingPeriod)
            {
                timer = 0;
                shoot();
            }
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

    // This is literally graphing it...
    IEnumerator CoroutineMovement(GameObject venom, float v0, float _Angle, float time) {
        float t = 0;
        while (t < time) {
            float x = v0 * t * Mathf.Cos(_Angle);
            float y = v0 * t * Mathf.Sin(_Angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            venom.transform.position = new Vector3(x,y,0) + transform.position;

            t+= Time.deltaTime;
            yield return null;
        }
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
        //DrawPath(v0, _Angle, time, _Step);
        //StopAllCoroutines();
        StartCoroutine(CoroutineMovement(newVenom, v0, _Angle, time));
    
    }
}
