using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

namespace LocalFolderSample.Data
{
    public class FileOperations
    {
        static ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;
        private static string _mruToken;
        private static string _tokenKey = "mruToken";

        private static async Task<StorageFile> CreateFile(string fileName)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.SuggestedFileName = fileName;
            savePicker.FileTypeChoices.Clear();
            savePicker.FileTypeChoices.Add("JSON", new List<string>() { ".json" });
            
            // Open the file save picker.
            var file = await savePicker.PickSaveFileAsync();
            // file is null if user cancels the file picker.
            return SaveMRU(file);
        }
  

        private static async Task<StorageFile> GetFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.ViewMode = PickerViewMode.List;
            
            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".json");
            // Open the file picker.
            var file = await openPicker.PickSingleFileAsync();
            return SaveMRU(file);
        }
        private static async Task<StorageFile> GetFileFromMRU()
        {
            return 
            await StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(_mruToken);
        }

        private static StorageFile SaveMRU(StorageFile file)
        {
            if (file != null)
            {
                _mruToken = StorageApplicationPermissions.MostRecentlyUsedList.Add(file);
                _settings.Values["mruToken"] = _mruToken;
                return file;
            }
            else
            {
                return null;
            }
        }

        public static async Task<StorageFile> OpenFile(string fileName)
        {
            _mruToken = (_settings.Values[_tokenKey] != null) ? 
                _settings.Values[_tokenKey].ToString() : null;
            if (_mruToken != null)
            {
                return await GetFileFromMRU();
            }
            var file = await GetFile();
            if (file != null)
            {
                return file;
            }
            else
            {
                return await CreateFile(fileName);
            }
        }
    }
}