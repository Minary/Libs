using System.Collections.Generic;

namespace MinaryLib.AttackService
{
  public interface IAttackService
  {

    #region PROPERTIES

    string ServiceName { get; set; }

    string WorkingDirectory { get; set; }

    IAttackServiceHost AttackServiceHost { get; set; }

    Dictionary<string, SubModule> SubModules { get; set; }

    ServiceStatus Status { get; set; }

    #endregion


    #region PUBLIC

    ServiceStatus StartService(ServiceParameters serviceParameters);

    ServiceStatus StopService();

    #endregion

  }
}
