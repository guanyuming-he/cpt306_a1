using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RangedEnemy : Enemy
{
    public RangedEnemy(int health, float x, float y, RangedAttack attack) 
        : base(health, x, y, attack)
    {
    }
}
