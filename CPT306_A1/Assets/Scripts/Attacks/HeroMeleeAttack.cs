using UnityEngine;

public sealed class HeroMeleeAttack : MeleeAttack
{
    private readonly AudioSource audioEffect;

    public HeroMeleeAttack
    (
        LevelObject src, 
        int dmg, float cd, 
        Vector2 attackRange, 
        GameObject visualEffect, AudioSource audioEffect
    )
        : base(src, dmg, cd, attackRange, visualEffect) 
    {
        this.audioEffect = audioEffect;
    }

    protected override void attack(Vector2 pos)
    {
        // Play the audio effect
        audioEffect.volume = AudioManager.effectsStrength();
        audioEffect.Play();

        base.attack(pos);
    }

    protected override bool canHit(GameObject obj)
    {
        var levelObj = obj.GetComponent<LevelObject>();
        if (levelObj == null)
        {
            return false;
        }

        // hero can only hit enemies and destructable obstacles.
        return levelObj is Enemy || levelObj is DesObstacle;
    }
}