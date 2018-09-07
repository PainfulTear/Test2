using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {

    public string Name;
    public int Type;
    public Sprite Icon;

    public float castRange;
    public float spendRate;

    public void OnCast1 ()
    {

    }

    public void OnCast2()
    {

    }

    public void OnCast3()
    {

    }
}

public class Slash : Ability
{

}

public class Repost : Ability
{

}

public class LowKick : Ability
{

}

public class RollDodge : Ability
{

}

public class ArcaneMissile : Ability
{

}

public class Heal : Ability
{

}