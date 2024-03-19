using p3rpc.sound.P3FESVoiceMod.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;

namespace p3rpc.sound.P3FESVoiceMod.Configuration
{
    public class Config : Configurable<Config>
    {
        /*
            User Properties:
                - Please put all of your configurable properties here.
    
            By default, configuration saves as "Config.json" in mod user config folder.    
            Need more config files/classes? See Configuration.cs
    
            Available Attributes:
            - Category
            - DisplayName
            - Description
            - DefaultValue

            // Technically Supported but not Useful
            - Browsable
            - Localizable

            The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.

        [DisplayName("Bool")]
        [Description("This is a bool.")]
        [DefaultValue(true)]
        public bool Boolean { get; set; } = true;
        */

        [DisplayName("NOT_IMPLEMENTED_Yet")]
        [Description("Yukari Takeba Voice Replacement Options")]
        [DefaultValue(SampleEnum.Default)]
        public SampleEnum Reloaded { get; set; } = SampleEnum.Default;

        public enum SampleEnum
        {
            NewDialogueMuted,
            NewDialogueUnMuted,
            Default,
            FUTURETHING
        }

        [DisplayName("Volume Slider")]
        [Description("This is a volume control slider.")]
        [DefaultValue(100)]
        [SliderControlParams(
            minimum: 0.0,
            maximum: 100.0,
            smallChange: 1.0,
            largeChange: 10.0,
            tickFrequency: 10,
            isSnapToTickEnabled: true,
            tickPlacement: SliderControlTickPlacement.BottomRight,
            showTextField: true,
            isTextFieldEditable: true,
            textValidationRegex: "\\d{1-3}")]
        public int VolumeSlider { get; set; } = 70;
    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}
