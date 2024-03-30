using UnityEngine;

public class HeroRangedAttack : RangedAttack
{
    private readonly AudioSource audioEffect;

    public HeroRangedAttack
    (
        LevelObject src, 
        int dmg, float cd, float projSpeed, 
        ProjSpawner projSpawner,
        AudioSource audioEffect
    ) 
        : base(src, dmg, cd, projSpeed, projSpawner)
    {
        Game.MyDebugAssert(src is Hero);
        this.audioEffect = audioEffect;
    }

    protected override void attack(Vector2 pos)
    {
        audioEffect.volume = AudioManager.effectsStrength();
        audioEffect.Play();

        base.attack(pos);
    }

    protected override Vector2 calcProjDirection()
    {
        var hero = getSrc() as Hero;
        return
            (new Vector2(hero.shootingCrossbar.transform.position.x, hero.shootingCrossbar.transform.position.y)
            - hero.getPos()).normalized;
    }
}