namespace MinaryLib.DataTypes
{
  using System;


  [Serializable]
  public class TemplatePluginData
  {

    #region PROPERTIES

    public byte[] PluginDataSearchPatternItems { get; set; }

    public byte[] PluginConfigurationItems { get; set; }

    #endregion


    #region PUBLIC

    public TemplatePluginData()
    {
    }

    public TemplatePluginData(byte[] pluginDataSearchPatternItems, byte[] pluginConfigurationItems)
    {
      this.PluginDataSearchPatternItems = pluginDataSearchPatternItems;
      this.PluginConfigurationItems = pluginConfigurationItems;
    }

    #endregion

  }
}
