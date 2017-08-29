namespace MinaryLib.AttackService
{
  using System.Collections.Generic;


  public class ServiceParameters
  {

    #region MEMBERS

    private int selectedIfcIndex;
    private string selectedIfcId;
    private Dictionary<string, string> targetList;

    #endregion


    #region PROPERTIES

    public int SelectedIfcIndex { get { return this.selectedIfcIndex; } set { this.selectedIfcIndex = value; } }
    public string SelectedIfcId { get { return this.selectedIfcId; } set { this.selectedIfcId = value; } }
    public Dictionary<string, string> TargetList { get { return this.targetList; } set { this.targetList = value; } }

    #endregion

  }
}
