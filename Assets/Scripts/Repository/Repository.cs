using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public abstract class Repository<TData>
{
    public event Action OnLoaded;
    protected string DefaultDirectoryPath => Path.Combine(Directory.GetCurrentDirectory(), "Saves");
    protected TData _data;
    public virtual TData GetData() => _data;
    protected abstract TData GetDefault();
    protected abstract string FileName();
    protected virtual string PathToDirectory() => DefaultDirectoryPath;
    private string FullPath => Path.Combine(PathToDirectory(), FileName());


    public void Save()
    {
        BeforeSave();
        var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(_data, Formatting.Indented);
        var path = FullPath;
        PrepareDirectory();
        File.WriteAllText(path, jsonText);
    }

    private void PrepareDirectory()
    {
        string path = PathToDirectory();
        if (Directory.Exists(path))
        {
            return;
        }
        else
        {
            Directory.CreateDirectory(path);
        }
    }

    public void Load()
    {
        var path = FullPath;
        PrepareDirectory();
        if (File.Exists(path))
        {
            var jsonText = File.ReadAllText(path);
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<TData>(jsonText);
            _data = obj;
        }
        else
        {
            Debug.Log("Сохранение отсутствует");
            _data = GetDefault();
        }

        AfterLoad();
        OnLoaded?.Invoke();
    }

    protected virtual void AfterLoad()
    {

    }

    protected virtual void BeforeSave()
    {

    }
}
