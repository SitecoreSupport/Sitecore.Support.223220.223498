using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Shell.Applications.ContentEditor.Dialogs.EditHtml.Commands;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using Sitecore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Sitecore.Support.Shell.Applications.ContentEditor.Dialogs.EditHtml.Commands
{
  [Serializable]
  public class Format : Sitecore.Shell.Applications.ContentEditor.Dialogs.EditHtml.Commands.Format
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull(context, "context");
      string text = EditHtmlCommandBase.GetHtml();
      try
      {
        text = XHtml.Convert(text);
        bool flag;
        try
        {
          XmlDocument xmlDocument = XmlUtil.LoadXml(text);
          flag = (xmlDocument == null);
        }
        catch (XmlException)
        {
          flag = true;
        }
        if (flag)
        {
          text = XHtml.MakeDocument(text, true);
        }
        string html = XHtml.Format(text);
        if (flag)
        {
          html = XHtml.GetBody(text);
        }
        EditHtmlCommandBase.SetHtml(html);
      }
      catch (Exception ex)
      {
        SheerResponse.Alert(string.Format("An error occured:\n\n{0}", ex.Message), new string[0]);
      }
    }
  }
}