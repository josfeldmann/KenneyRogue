using UnityEngine;

public class ProjectilePoint : MonoBehaviour {
    public Projectile projectile;
    public LayerMask mask;
    public float shootDelay;
    public float damage = 1;
    public float speed = 1;
    private float angleOffset;

    public void SetMask(LayerMask mask) {
        this.mask = mask;
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    public void SetSpeed(float s) {
        this.speed = s;
    }

    private void Awake() {
        angleOffset = transform.eulerAngles.z;
    }

    public void Fire(Vector3 vec) {
        Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        
        vec.z = 0;

        p.transform.right = (vec - transform.position).normalized;
        p.damage = damage;
        p.speed = speed;
        p.Init(mask);
        p.transform.eulerAngles += new Vector3(0, 0, angleOffset);
        
    }

} 
