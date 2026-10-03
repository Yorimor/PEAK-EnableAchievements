using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace peakAchievementsEnable;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
public class EnableAchievements : BaseUnityPlugin
{   
    public static EnableAchievements Instance { get; private set; } = null!;
    internal static ManualLogSource Log { get; private set; } = null!;
    
    public const string PLUGIN_GUID = "Yorimor.EnableAchievements";
    public const string PLUGIN_NAME = "EnableAchievements";
    public const string PLUGIN_VERSION = "1.0.1";

    private void Awake()
    {
        Instance = this;
        var harmony = new Harmony(PLUGIN_GUID);
        harmony.PatchAll(typeof(RunSettingsPatches));
        
        Logger.LogInfo($"Plugin {PLUGIN_NAME} is loaded!");
    }
}

[HarmonyPatch(typeof(RunSettings))]
public class RunSettingsPatches
{
    [HarmonyPatch("blockingAchievements", MethodType.Getter)]
    [HarmonyPostfix]
    private static void AllowAchievements(ref bool __result)
    {
        __result = false;
    }
}