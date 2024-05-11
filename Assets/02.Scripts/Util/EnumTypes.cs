using UnityEngine;

namespace EnumTypes
{
    public enum LookDirection { Up, Down, Left, Right }
    public enum PlayerState { Stand, Move, Run, Cook }
    public enum Layers
    {
        Default,
        TransparentFX,
        IgnoreRaycast,
        Reserved1,
        Water,
        UI,
        Reserved2,
        Reserved3,
        Player,
        World,
    }

    public enum ThrowableType
    {
        Ingredient,
    };

    public enum GlobalEvents
    {
        PlayerDead,
        PlayerSpawned,
        PlayerStabbed,
        PlayerInactive,
        PlayerDamaged,
    }

    public class EnumTypes : MonoBehaviour { }
}