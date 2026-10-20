using System.IO;
using PCL.Core.App;
using PCL.Core.App.Localization;

namespace PCL;

/// <summary>
///     启动器版本信息。
///     本分支已完全移除更新检测与在线更新功能：不存在任何远程版本源，
///     不会联网检查新版本，也不会下载或覆盖当前程序。
/// </summary>
public static class UpdateManager
{
    public static bool IsCurrentVersionBeta
    {
        get
        {
            if (ModBase.versionBaseName.Contains("beta"))
                return true;
            return (int)Config.Update.UpdateChannel == 1;
        }
    }

    /// <summary>
    ///     获取当前版本状态。本分支不进行任何更新检测，直接视为最新版本。
    /// </summary>
    public static UpdateEnums.VersionStatus GetVersionStatus()
    {
        return UpdateEnums.VersionStatus.Latest;
    }

    /// <summary>
    ///     已移除的更新入口。保留该方法是为了让任何残留调用都变成安全的空操作，而不是联网检查。
    /// </summary>
    public static void UpdateStart(UpdateEnums.UpdateType type, string receivedKey = null, bool forceValidated = false)
    {
        ModBase.Log("[Update] 更新功能已在此分支中移除，忽略更新请求");
    }

    /// <summary>
    ///     确保 PathTemp 下的 CE-Latest.exe 可被用于整合包打包。
    ///     本分支不再下载远程启动器，而是直接复用当前正在运行的程序。
    /// </summary>
    internal static void DownloadLatestPCL(ModLoader.LoaderBase loaderToSyncProgress = null)
    {
        var latestPCLPath = Path.Combine(ModBase.pathTemp, "CE-Latest.exe");
        ModBase.Log("[Update] 更新功能已移除，使用当前程序作为整合包内置启动器");
        ModBase.CopyFile(Basics.ExecutablePath, latestPCLPath);
    }

    /// <summary>
    ///     启动器后台服务。本分支不再拉取任何在线信息，仅保留一个空任务以兼容启动流程。
    /// </summary>
    public static ModLoader.LoaderTask<int, int> serverLoader =
        new(Lang.Text("Update.Service.PclCe"),
            _ => ModBase.Log("[Update] 更新检测已禁用，跳过在线检查"),
            priority: ThreadPriority.BelowNormal);

    /// <summary>
    ///     展示社区版提示
    /// </summary>
    public static void ShowCEAnnounce()
    {
        ModMain.MyMsgBox(Lang.Text("Update.CommunityNotice.Body"),
            Lang.Text("Update.CommunityNotice.Title"),
            Lang.Text("Update.CommunityNotice.Confirm"));
    }
}
