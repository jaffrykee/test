

namespace TkmGame.Gtr.Battle {
    public delegate void EffectFunc(Unit self, Unit unit);
    public enum EffectEvent {
        onBorn = 0,
        onEnter,
        onDead,
        onExit,
        onOtherUnitEnter,
        onOtherUnitDead,
        onUnitInjured,
        onAttack,
        onAttacked,
        onSelfTurnBegin,
        onSelfTurnEnd,
        onEnemyTurnBegin,
        onEnemyTurnEnd,
        MAX,
    };
}
