namespace Minary.FirewallManager
{
  using System.Collections.Generic;
  using System.Diagnostics;


  public class NetshFirewallManager
  {

    #region PUBLIC METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    /// <param name="portNumber"></param>
    public void CreateFirewallRulePort(string ruleName, int portNumber)
    {
      var netshPortCommandstring = $@"advfirewall firewall add rule name=""{ruleName}"" dir=in action=allow protocol=tcp localport={portNumber}";
      this.RunNetsh(netshPortCommandstring);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="ruleName"></param>
    /// <param name="binaryPath"></param>
    public void CreateFirewallRuleApplication(string ruleName, string binaryPath)
    {
      var netshApplicationCommandstring = $@"advfirewall firewall add rule name=""{ruleName}"" dir=in action=allow program=""{binaryPath}""";
      this.RunNetsh(netshApplicationCommandstring);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="firewallRuleName"></param>
    public void DeleteFirewallRule(string firewallRuleName)
    {
      var netshApplicationCommandstring = $@"advfirewall firewall delete rule name=""{firewallRuleName}"" ";
      this.RunNetsh(netshApplicationCommandstring);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param name="firewallRuleName"></param>
    /// <returns></returns>
    public bool FirewallRuleExists(string firewallRuleName)
    {
      var retVal = false;
      var procNetsh = new ProcessStartInfo();
      var netshArguments = $@"advfirewall firewall show rule name=""{firewallRuleName}""";

      // Open incoming port for application
      procNetsh.FileName = "netsh.exe";
      procNetsh.Arguments = netshArguments;
      procNetsh.UseShellExecute = false;
      procNetsh.CreateNoWindow = true;
      procNetsh.WindowStyle = ProcessWindowStyle.Normal;
      procNetsh.RedirectStandardOutput = true;

      using (var process = Process.Start(procNetsh))
      {
        using (var reader = process.StandardOutput)
        {
          var line = string.Empty;
          var outputLines = new List<string>();

          while (!process.StandardOutput.EndOfStream)
          {
            line = reader.ReadLine();
            outputLines.Add(line);
          }

          foreach (string tmpLine in outputLines)
          {
            if (tmpLine.ToLower().Contains(firewallRuleName.ToLower()))
            {
              retVal = true;
              break;
            }
          }
        }
      }

      return retVal;
    }

    #endregion


    #region PRIVATE METHODS

    /// <summary>
    ///
    /// </summary>
    /// <param name="pNetshArguments"></param>
    private void RunNetsh(string netshArguments)
    {
      var procNetsh = new Process();

      // Open incoming port for application
      procNetsh.StartInfo.FileName = "netsh.exe";
      procNetsh.StartInfo.Arguments = netshArguments;
      procNetsh.StartInfo.UseShellExecute = false;
      procNetsh.StartInfo.RedirectStandardOutput = true;
      procNetsh.StartInfo.CreateNoWindow = true;
      procNetsh.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
      procNetsh.Start();

      if (procNetsh.WaitForExit(2000) == false)
      {
        procNetsh.Kill();
      }
    }

    #endregion

  }
}