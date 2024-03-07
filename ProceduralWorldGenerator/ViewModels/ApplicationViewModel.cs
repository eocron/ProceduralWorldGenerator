using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ApplicationViewModel : ObservableObject
    {
        [JsonProperty]
        public NodifyObservableCollection<EditorViewModel> Editors { get; } = new();

        public ApplicationViewModel()
        {
            AddEditorCommand = new DelegateCommand(() => Editors.Add(new EditorViewModel
            {
                Name = $"Editor {Editors.Count + 1}"
            }));
            CloseEditorCommand = new DelegateCommand<string>(
                id => Editors.RemoveOne(editor => editor.Id == id),
                _ => Editors.Count > 0 && SelectedEditor != null);
            NewProjectCommand = new DelegateCommand(() => { });
            OpenProjectCommand = new DelegateCommand(OnOpenProjectCommand);
            SaveProjectCommand = new DelegateCommand(OnSaveProjectCommand);
            SaveAsProjectCommand = new DelegateCommand(OnSaveAsProjectCommand);
            
            Editors.WhenAdded((editor) =>
            {
                if (AutoSelectNewEditor || Editors.Count == 1)
                {
                    SelectedEditor = editor;
                }
            });
            Editors.Add(new EditorViewModel
            {
                Name = $"Editor {Editors.Count + 1}"
            });
        }

        private void OnSaveAsProjectCommand()
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = "*.pwgproj";
            dialog.AddExtension = true;
            dialog.Multiselect = false;
            dialog.Filter = "PWG file|*.pwgproj";
            dialog.Title = "Save pwgproj to";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == true)
            {
                var tmp = new ProjectSettingsViewModel()
                {
                    ProjectFilePath = dialog.FileName
                };
                SaveProject(tmp);
                ProjectInfo = tmp;
            }
        }

        private void OnSaveProjectCommand()
        {
            if (ProjectInfo == null)
            {
                OnSaveAsProjectCommand();
                return;
            }
            
            SaveProject(ProjectInfo);
        }

        private void OnOpenProjectCommand()
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = "*.pwgproj";
            dialog.Multiselect = false;
            dialog.Filter = "PWG file|*.pwgproj";
            dialog.Title = "Open pwgproj file...";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            if (dialog.ShowDialog() == true)
            {
                var tmp = new ProjectSettingsViewModel()
                {
                    ProjectFilePath = dialog.FileName
                };
                LoadProject(tmp);
                ProjectInfo = tmp;
            }
        }

        private void LoadProject(ProjectSettingsViewModel settings)
        {
            try
            {
                var serialized = File.ReadAllText(settings.ProjectFilePath);
                var deserialized = CommonViewModelSerializer.Deserialize<ApplicationViewModel>(serialized);
                

                //TODO
            }
            catch(Exception e)
            {
                Trace.TraceError(e.ToString());
            }
        }

        private void SaveProject(ProjectSettingsViewModel settingsViewModel)
        {
            //TODO
            var json = CommonViewModelSerializer.Serialize(this);
            if (!Directory.Exists(settingsViewModel.ProjectFolderPath))
            {
                Directory.CreateDirectory(settingsViewModel.ProjectFolderPath);
            }
            File.WriteAllText(settingsViewModel.ProjectFilePath, json);
            ProjectInfo = settingsViewModel;
        }

        public ProjectSettingsViewModel ProjectInfo { get; set; }

        public ICommand AddEditorCommand { get; }
        public ICommand CloseEditorCommand { get; }
        public ICommand NewProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }

        private EditorViewModel? _selectedEditor;
        public EditorViewModel? SelectedEditor
        {
            get => _selectedEditor;
            set => SetProperty(ref _selectedEditor, value);
        }

        private bool _autoSelectNewEditor = true;
        public bool AutoSelectNewEditor
        {
            get => _autoSelectNewEditor;
            set => SetProperty(ref _autoSelectNewEditor , value); 
        }
    }
}
