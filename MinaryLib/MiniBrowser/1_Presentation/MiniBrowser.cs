using miniBrowserTypes = MinaryLib.MiniBrowser.DataTypes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Minary.MiniBrowser
{

  public partial class Browser : Form
  {

    #region IMPORTS

    [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

    [DllImport("wininet.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "DeleteUrlCacheEntryA", CallingConvention = CallingConvention.StdCall)]
    public static extern bool DeleteUrlCacheEntry(string lpszUrlName);

    [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
    private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved); const int URLMON_OPTION_USERAGENT = 0x10000001;

    #endregion


    #region MEMBERS
    
    private string userAgentCustom;
    private string cookies;
    private TaskFacade taskLayer;
    private List<miniBrowserTypes.UserAgent> userAgentList = new List<miniBrowserTypes.UserAgent>();

    #endregion


    #region PUBLIC

    /// <summary>
    /// Initializes a new instance of the <see cref="Browser"/> class.
    ///
    /// </summary>
    /// <param firewallRuleName="url"></param>
    /// <param firewallRuleName="cookie"></param>
    /// <param firewallRuleName="srcIp"></param>
    /// <param firewallRuleName="userAgent"></param>
    public Browser(string url, string cookie, string srcIp, string userAgent)
    {
      this.InitializeComponent();

      if (string.IsNullOrEmpty(userAgent) == false &&
          string.IsNullOrWhiteSpace(userAgent) == false)
      {
        this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "CUSTOM", Value = userAgent });
      }

      this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "Edge/Win 14.14", Value = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14931" });
      this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "FF/Win 64.0", Value = "Mozilla/5.0 (X11; Linux i686; rv:64.0) Gecko/20100101 Firefox/64.0" });
      this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "FF/Lin 64.0", Value = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:64.0) Gecko/20100101 Firefox/64.0" });
      this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "Chrome/Win", Value = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36" });
      this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "Opera/Win 12.14", Value = "Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14" });
      this.userAgentList.Add(new miniBrowserTypes.UserAgent() { Name = "OSX/Safari 7.0.3", Value = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A" });    

      this.taskLayer = TaskFacade.GetInstance();
      this.cmb_UserAgent.DataSource = this.userAgentList;
      this.cmb_UserAgent.DisplayMember = "Name";
      this.cmb_UserAgent.ValueMember = "Value";
      this.cmb_UserAgent.SelectedIndex = 0;
      this.tb_UserAgent.Text = this.userAgentList[0].Value;

      this.tb_URL.Text = url;
      this.tb_Cookies.Text = cookie;
      this.cookies = cookie;

      this.userAgentCustom = userAgent;
      this.cmb_UserAgent.SelectedIndex = 0;
      this.Text = "MiniBrowser 0.3";

      if (!string.IsNullOrEmpty(url))
      {
        if (!url.ToLower().StartsWith("http"))
        {
          url = $"http://{url}";
          this.tb_URL.Text = url;
        }
      }

      var requestedUrl = this.tb_URL.Text;
      
      this.taskLayer.ClearIECache();
      this.taskLayer.ClearCookies();

      if (this.tb_Cookies.Text.Length > 0)
      {
        try
        {
          foreach (string tmpCookie in this.tb_Cookies.Text.ToString().Split(';'))
          {
            if (tmpCookie.Length > 0 && tmpCookie.Contains("="))
            {
              Regex regex = new Regex("=");
              string[] substrings = regex.Split(tmpCookie, 2);

              InternetSetCookie(requestedUrl, substrings[0], substrings[1]);
            }
          }
        }
        catch (Exception)
        {
        }
      }

      DeleteUrlCacheEntry(requestedUrl);
      this.taskLayer.ClearIECache();
      this.taskLayer.ClearCookies();

      UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, this.tb_UserAgent.Text, this.tb_UserAgent.Text.Length, 0);
    }

    #endregion

  }
}
