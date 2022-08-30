using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public bool seeking = false;
    public Rigidbody2D rb;
    public Seeker seeker;
    public float speed = 5;
    int currentWaypoint = 0;
    public float stopDistance = 0.25f;
    Path path;
    private void Awake() {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }
    public void Stop() {

        rb.velocity = Vector2.zero;
    }

    public void StartOnPath(Path p) {
        this.path = p;
        currentWaypoint = 0;
        seeking = true;
    }

    public void SetPosition(Vector3 start, Vector3 end) {
        seeker.StartPath(start, end, StartOnPath);
        
    }


    public void Update() {
        if (!seeking) return;
        if (currentWaypoint >= path.vectorPath.Count) {
            seeking = false;
            rb.velocity = Vector2.zero;
            return;
        }

        float distance = Vector3.Distance(path.vectorPath[currentWaypoint], transform.position);

        if (distance < stopDistance) {
            currentWaypoint++;
            if (currentWaypoint >= path.vectorPath.Count) {
                seeking = false;
                rb.velocity = Vector2.zero;
                return;
            }
        }

        Vector2 dir = (((Vector2)path.vectorPath[currentWaypoint]) - rb.position).normalized;
        rb.velocity = dir * speed;

    }




}
