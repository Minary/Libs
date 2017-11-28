namespace MinaryLib.AttackService.Interface
{

  public interface IAttackServiceHost
  {

    #region PROPERTY

    bool IsDebuggingOn { get; }

    #endregion PROPERTY


    #region METHODS

    void Register(IAttackService attackService);

    void LogMessage(string message, params object[] formatArgs);

    void OnServiceExited(string serviceName, int exitCode);

    #endregion

  }
}
