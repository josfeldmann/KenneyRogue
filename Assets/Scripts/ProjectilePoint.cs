using UnityEngine;

public class ProjectilePoint : MonoBehaviour {
    public Projectile projectile;
    public LayerMask mask;
    public float shootDelay;
    private float angleOffset;

    public void SetMask(LayerMask mask) {
        this.mask = mask;
    }

    private void Awake() {
        angleOffset = transform.eulerAngles.z;
    }

    public void Fire(Vector3 vec) {
        Projectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        
        vec.z = 0;

        p.transform.right = (vec - transform.position).normalized;
        p.Init(mask);
        p.transform.eulerAngles += new Vector3(0, 0, angleOffset);
        
    }

} 
