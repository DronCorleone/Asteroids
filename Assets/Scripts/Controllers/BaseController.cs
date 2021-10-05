public class BaseController : IInitialize
{
    protected bool _isActive = true;
    protected MainController _main;

    public MainController Main => _main;
    public bool IsActive => _isActive;

    public BaseController(MainController main)
    {
        main.AddController(this);
        _main = main;
        CustomDebug.Log($"{this.GetType()} added in controller list");
    }

    public virtual void Initialize()
    {

    }

    protected virtual void SetState(bool state)
    {
        _isActive = state;
    }

    public virtual void Enable()
    {
        SetState(true);
    }
    public virtual void Disable()
    {
        SetState(false);
    }
}