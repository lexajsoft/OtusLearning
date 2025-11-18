using System;
using System.Collections.Generic;
using System.IO;
using Extension;
using Newtonsoft.Json;
using UnityEngine;

namespace Saver
{
    public class SavingService : ISaving
    {
        private string FileName => "Save.dat";
        private string DirectoryPath => Directory.GetCurrentDirectory();
        private string FullPath => Path.Combine(DirectoryPath, FileName);
        private string LabelService => "[SaveService]";
    
        private Dictionary<Type, ISaveable> _savables;
        private List<ISavingHandler> _handlers = new List<ISavingHandler>();
        private Dictionary<string, string> _data = new();
        private ICrypto _crypto;
        public SavingService()
        {
            _savables = new Dictionary<Type, ISaveable>();
            _crypto = new AesEncryption("my-secret-key-#00000001", "initial-vector-16");
        }

        public void Save()
        {
            Log("Save-Start");
            Dictionary<string, string> dataSnapshot = new Dictionary<string, string>();
            List<ISavingHandler> handlersSnapshot = new List<ISavingHandler>(_handlers);

            // Обращаемся к каждому обработчику и берем данные которые там есть, после чего помещаем их в словарь под ключом с данными
            foreach (ISavingHandler savingHandler in handlersSnapshot)
            {
                string processorData = JsonConvert.SerializeObject(savingHandler.GetValue());
                dataSnapshot[savingHandler.SaveKey] = processorData;
            }

            
            string json = JsonConvert.SerializeObject(dataSnapshot);

            var text = Crypt(json);
            File.WriteAllText(FullPath, text);
            Log("Save-Complete");
        }

        private string Crypt(string jsonText)
        {
            var cryptoText = _crypto.EncryptJson(jsonText);
            return cryptoText;
        }

        private string DeCrypt(string text)
        {
            var decryptedText = _crypto.DecryptJson<string>(text);
            return decryptedText;
        }

        public void Load()
        {
            Log("Load-Start");
            if (File.Exists(FullPath))
            {
                
                var text = File.ReadAllText(FullPath);
                var jsonText = DeCrypt(text);
                _data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
                UpdateHandlers();
                Log("Load-Complete");
            }
            else
            {
                Log("Load-Error NotFile");
            }
        }

        public void Register(ISavingHandler savingHandler)
        {
            Log($"Try Register:[{savingHandler.GetType().ConvertTypeToKeyName()}]");
            if (_handlers.Contains(savingHandler))
                return;

            _handlers.Add(savingHandler);

            UpdateHandler(savingHandler);
        }

        private void UpdateHandler(ISavingHandler savingHandler)
        {
            Log($"Try Update:[{savingHandler.GetType().ConvertTypeToKeyName()}]");
            if (!_data.ContainsKey(savingHandler.SaveKey))
                _data.Add(savingHandler.SaveKey, GetProcessorData(savingHandler));

            object saveObject = JsonConvert.DeserializeObject(_data[savingHandler.SaveKey], savingHandler.SaveType);
            savingHandler.SetObjectValue(saveObject);
        }

        
        
        private string GetProcessorData(ISavingHandler savingHandler)
        {
            return JsonConvert.SerializeObject(savingHandler.GetValue());
        }
        
        private void UpdateHandlers()
        {
            foreach (ISavingHandler processor in _handlers)
                UpdateHandler(processor);
        }

        public void UnRegister(ISavingHandler savingHandler)
        {
            _handlers.Remove(savingHandler);
        }

        private void Log(string str)
        {
            Debug.Log(LabelService+str);
        }
    }
}
