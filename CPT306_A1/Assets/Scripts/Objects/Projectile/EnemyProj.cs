public sealed class EnemyProj : Projectile
{
    protected override void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        var other = collision.otherCollider.gameObject;
        // enemy can only hit obstacle and the hero
        if (other.GetComponent<Hero>() != null || other.GetComponent<DesObstacle>() != null)
        {
            var hittable = other.GetComponent<IHittable>();
            // obstacles do not have this
            if (hittable != null)
            {
                hittable.onHit(dmg, src);
            }
            this.destroy();
        }
    }
}