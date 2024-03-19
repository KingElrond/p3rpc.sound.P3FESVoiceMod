using p3rpc.sound.P3FESVoiceMod.Configuration;
using p3rpc.sound.P3FESVoiceMod.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Loader.IO;
using Reloaded.Mod.Loader.IO.Config;
using Reloaded.Mod.Loader.IO.Utility;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace p3rpc.sound.P3FESVoiceMod
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {
        /// <summary>
        /// Provides access to the mod loader API.
        /// </summary>
        private readonly IModLoader _modLoader;

        /// <summary>
        /// Provides access to the Reloaded.Hooks API.
        /// </summary>
        /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
        private readonly IReloadedHooks? _hooks;

        /// <summary>
        /// Provides access to the Reloaded logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Entry point into the mod, instance that created this class.
        /// </summary>
        private readonly IMod _owner;

        /// <summary>
        /// Provides access to this mod's configuration.
        /// </summary>
        private Config _configuration;

        /// <summary>
        /// The configuration of the currently executing mod.
        /// </summary>
        private readonly IModConfig _modConfig;

        public Mod(ModContext context)
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;

            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic

        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;
            _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
            var configIO = IConfig<LoaderConfig>.FromPathOrDefault(Paths.LoaderConfigPath);
            var LauncherPath = Paths.GetLoaderFolder64(configIO.LauncherPath);
            var ParentFolderPath = Directory.GetParent(Directory.GetParent(LauncherPath).FullName).FullName;
            var ModFolderPath = Path.Combine(Path.Combine(ParentFolderPath, "Mods"), "p3rpc.sound.P3FESVoiceMod");
            var RyoP3RFolderPath = Path.Combine(Path.Combine(ModFolderPath, "Ryo"), "P3R");
            var configFilePath = Path.Combine(RyoP3RFolderPath, "config.yaml");

            // Try to open the config.yaml file stream
            String yamlContent;
            using (var fileStream = IOEx.TryOpenOrCreateFileStream(configFilePath, FileMode.Open, FileAccess.Read))
            {
                if (fileStream == null)
                {
                    Console.WriteLine("Failed to open config.yaml file.");
                    return;
                }

                using (var reader = new StreamReader(fileStream))
                {
                    yamlContent = reader.ReadToEnd();
                }
            }

            // Deserialize YAML content to a dictionary
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yamlObject = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);

            // Modify the volume parameter
            if (yamlObject.TryGetValue("volume", out var volumeValue))
            {
                yamlObject["volume"] = _configuration.VolumeSlider / 100; // Change the volume to the desired value
            }
            else
            {
                Console.WriteLine("Volume parameter not found in config.yaml file.");
                return;
            }

            // Serialize the modified YAML content
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var modifiedYamlContent = serializer.Serialize(yamlObject);

            // Write the modified YAML content back to the config.yaml file
            using (var fileStream = IOEx.TryOpenOrCreateFileStream(configFilePath, FileMode.Create, FileAccess.Write))
            {
                if (fileStream == null)
                {
                    Console.WriteLine("Failed to open config.yaml file for writing.");
                    return;
                }

                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(modifiedYamlContent);
                }
            }

            Console.WriteLine("Volume parameter updated successfully.");
        }
        #endregion

        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Mod() { }
#pragma warning restore CS8618
        #endregion
    }
}