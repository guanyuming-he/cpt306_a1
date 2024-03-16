using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MeleeEnemy : Enemy
{
    public MeleeEnemy(int health, float x, float y, MeleeAttack attack) 
        : base(health, x, y, attack)
    {
    }
}

