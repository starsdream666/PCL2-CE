using PCL.Core.App.Localization;
using PCL.Core.Utils;

namespace PCL;

/// <summary>
///     启动器版本页面。
///     本分支已移除更新检测与在线更新，此页面只展示本地版本信息。
/// </summary>
public partial class PageSetupUpdate
{
    public PageSetupUpdate()
    {
        InitializeComponent();
        Loaded += (_, _) => Init();
    }

    private void Init()
    {
        TextCurrentVersion.Text = "PCL CE " + VersionNameFormat(ModBase.versionBaseName);
        TextCurrentDesc.Text = Lang.Text("Setup.Update.Offline.Desc");
        TextVersionCode.Text = Lang.Text("Setup.Update.Offline.VersionCode", ModBase.versionCode, ModBase.versionBranchName);
        TextOfflineNote.Text = Lang.Text("Setup.Update.Offline.Note");
    }

    public string VersionNameFormat(string str)
    {
        str = str.Replace("v", "");
        if (!str.Contains("-"))
            return str;
        var add = str.AfterLast("-");
        str = str.BeforeLast("-");
        return $"{str} {add.Replace(".", " ").Replace("beta", "Beta").Replace("rc", "RC")}";
    }
}
