namespace MinaryLib.AttackService.Class
{
  public class SubModule
  {

    #region MEMBERS

    public string ModuleName { get; set; }

    public string WorkingDirectory { get; set; }

    public string ConfigFilePath { get; set; }

    #endregion


    #region PUBLIC

    public SubModule(string moduleName, string workingDirectory, string configFilePath)
    {
      this.ModuleName = moduleName;
      this.WorkingDirectory = workingDirectory;
      this.ConfigFilePath = configFilePath;
    }

    #endregion

  }
}
