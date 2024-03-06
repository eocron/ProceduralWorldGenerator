using System.IO;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProjectSettingsViewModel
    {
        [JsonProperty]
        public string ProjectFilePath { get; init; }

        public string? ProjectFolderPath => string.IsNullOrWhiteSpace(ProjectFilePath) ? null : Path.GetDirectoryName(ProjectFilePath);
    }
}