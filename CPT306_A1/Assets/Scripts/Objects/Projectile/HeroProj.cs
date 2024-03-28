public sealed class HeroProj : Projectile
{
    protected override void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        var hit = collision.collider.gameObject;
        // hero can only hit obstacle and the enemies
        if(hit.GetComponent<Enemy>() != null || hit.GetComponent<Obstacle>() != null)
        {
            var hittable = hit.GetComponent<IHittable>();
            // obstacles do not have this
            if (hittable != null)
            {
                hittable.onHit(dmg, src);
            }

            this.destroy();
        }
    }
}