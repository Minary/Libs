namespace MinaryLib.AttackService.Class
{
  using System.Collections.Generic;


  public class StartServiceParameters
  {

    #region PROPERTIES

    public int SelectedIfcIndex { get; set; }

    public string SelectedIfcId { get; set; }

    public Dictionary<string, string> TargetList { get; set; }

    #endregion

  }
}
