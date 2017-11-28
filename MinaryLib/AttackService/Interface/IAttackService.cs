namespace MinaryLib.AttackService.Interface
{
  using MinaryLib.AttackService.Class;
  using MinaryLib.AttackService.Enum;


  public interface IAttackService
  {

    #region PROPERTIES

    string ServiceName { get; set; }

    ServiceStatus Status { get; set; }

    #endregion


    #region PUBLIC

    ServiceStatus StartService(StartServiceParameters serviceParameters);

    ServiceStatus StopService();

    #endregion

  }
}
