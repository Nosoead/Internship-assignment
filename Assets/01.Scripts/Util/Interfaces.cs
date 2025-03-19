public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public interface IDamageable
{
    void TakeDamage(float damage);
}

public interface IInteractable
{
    int Interact();
}

public interface ISingletonInitializer
{
    void Init();
}