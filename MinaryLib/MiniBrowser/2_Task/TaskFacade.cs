using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Minary.MiniBrowser
{

  public class TaskFacade
  {

    #region MEMBERS

    private static TaskFacade instance;

    #endregion


    #region PUBLIC

    /// <summary>
    /// Create single instance
    /// </summary>
    /// <returns></returns>
    public static TaskFacade GetInstance()
    {
      if (instance == null)
      {
        instance = new TaskFacade();
      }

      return (instance);
    }


    /// <summary>
    ///
    /// </summary>
    public void ClearIECache()
    {
      var process = new Process();
      process.StartInfo.FileName = "cmd.exe";
      process.StartInfo.Arguments = "/c " + "del /f /s /q \"%userprofile%\\Local Settings\\Temporary Internet Files\\*.*\"";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      Application.DoEvents();
    }



    /// <summary>
    ///
    /// </summary>
    public void ClearCookies()
    {
      var dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
      foreach (FileInfo info in dir.GetFiles("*.txt"))
      {
        info.Delete();
        Application.DoEvents();
      }
    }

    #endregion


    #region PRIVATE
    
    private TaskFacade()
    {
    }

    #endregion

  }
}
