using miniBrowserTypes = MinaryLib.MiniBrowser.DataTypes;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Minary.MiniBrowser
{
  public partial class Browser
  {

    #region MEMBERS

    private static string headerData;

    #endregion


    #region EVENTS

    /// <summary>
    ///
    /// </summary>
    /// <param firewallRuleName="sender"></param>
    /// <param firewallRuleName="e"></param>
    private void BT_Open_Click(object sender, EventArgs e)
    {
      var url = this.tb_URL.Text;
      var host = string.Empty;
      var tmpHost = string.Empty;

      if (string.IsNullOrEmpty(url) == false &&
          url.Contains(Uri.SchemeDelimiter) == false)
      {
        url = string.Concat(Uri.UriSchemeHttp, Uri.SchemeDelimiter, url);
        this.tb_URL.Text = url;
      }

      try
      {
        Uri uri = new Uri(url);
        host = uri.Host;
      }
      catch (Exception)
      {
      }

      headerData = string.Empty;
      this.taskLayer.ClearIECache();
      this.taskLayer.ClearCookies();

      if (this.tb_Cookies.Text.Length > 0)
      {
        try
        {
          foreach (string tmpCookie in this.tb_Cookies.Text.ToString().Split(';'))
          {
            if (tmpCookie.Length > 0 &&
                tmpCookie.Contains("="))
            {
              Regex regex = new Regex("=");
              string[] substrings = regex.Split(tmpCookie, 2);

              InternetSetCookie(url, substrings[0], substrings[1]);
            }
          }
        }
        catch (Exception)
        {
        }
      }

      headerData = "User-Agent: " + this.tb_UserAgent.Text + "\r\n";
      headerData = "Host: " + host + "\r\n";

      if (this.tb_Cookies.Text.Length > 0)
      {
        headerData += "Cookie: " + this.tb_Cookies.Text + "\r\n";
      }

      DeleteUrlCacheEntry(url);
      this.taskLayer.ClearIECache();
      this.taskLayer.ClearCookies();

      UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, this.tb_UserAgent.Text, this.tb_UserAgent.Text.Length, 0);
      this.wb_MiniBrowser.ScriptErrorsSuppressed = true;
      this.wb_MiniBrowser.Navigate(url, string.Empty, null, headerData);
    }


    /// <summary>
    ///
    /// </summary>
    /// <param firewallRuleName="isEnabled"></param>
    public delegate void ActivateGBDetailsDelegate(bool pEnabled);
    public void ActivateGBDetails(bool isEnabled)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new ActivateGBDetailsDelegate(this.ActivateGBDetails), new object[] { isEnabled });
        return;
      }

      if (isEnabled == false)
      {
        this.gb_Details.Enabled = false;
        //// gb_WebPage.Enabled = false;
        //// Cursor = Cursors.WaitCursor;
      }
      else
      {
        this.gb_Details.Enabled = true;
        //// gb_WebPage.Enabled = true;
        //// Cursor = Cursors.Default;
      }
    }


    /// <summary>
    ///  HTTP request Access token.
    ///  This is the tricky part! If somebody knows an easier way to get an AccessToken
    ///  -> let me know.
    /// </summary>
    /// <param firewallRuleName="sender"></param>
    /// <param firewallRuleName="e"></param>
    private void BGW_GetAccessToken_DoWork(object sender, DoWorkEventArgs e)
    {
    }


    /// <summary>
    ///
    /// </summary>
    /// <param firewallRuleName="sender"></param>
    /// <param firewallRuleName="e"></param>
    private void TB_URL_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.SuppressKeyPress = true;
        this.BT_Open_Click(null, null);
      }
    }


    /// <summary>
    ///
    /// </summary>
    /// <param firewallRuleName="sender"></param>
    /// <param firewallRuleName="e"></param>
    private void CMB_UserAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        var selectedUserAgent = (miniBrowserTypes.UserAgent)this.cmb_UserAgent.SelectedItem;
        int resultIndex = this.cmb_UserAgent.FindStringExact(selectedUserAgent.Name);

        if (resultIndex < 0)
        {
          return;
        }

        var name = this.userAgentList[resultIndex].Name;
        var value = this.userAgentList[resultIndex].Value;

        this.tb_UserAgent.Text = value;
      }
      catch (Exception ex)
      {
        MessageBox.Show($"MB(EXC): {ex.Message}\r\n\r\n{ex.StackTrace}");
      }
    }

    #endregion

  }
}
