namespace MinaryLib.AttackService.Class
{
  using MinaryLib.AttackService.Interface;
  using System.Collections.Generic;


  public class AttackServiceParameters
  {

    #region PROPERTIES

    public IAttackServiceHost AttackServiceHost { get; set; }

    public string AttackServicesWorkingDirFullPath { get; set; }

    public Dictionary<string, SubModule> PluginSubModules { get; set; }

    public string PipeName { get; set; }

    #endregion

  }
}
