using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float rocketSpeed = 40f;
    private Vector3 rocketDir;

    private void Update()
    {
        transform.position += rocketDir * rocketSpeed * Time.deltaTime;
    }
    public void RocketDirection(Vector3 dir)
    {
        rocketDir = dir;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
