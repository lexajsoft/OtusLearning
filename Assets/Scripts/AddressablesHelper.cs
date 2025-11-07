using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesHelper : MonoBehaviour
{
    // Асинхронная загрузка файла из адресоблов по указанному названию, с оберткой в uniTask
    public static async UniTask<T> LoadAsync<T>(string name, CancellationToken cancellationToken)
    {
        return await Addressables.LoadAssetAsync<T>(name).WithCancellation(cancellationToken);
    }

    /// <summary>
    /// Получение имен всех файлов с пометкой этого лейбла
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    public static async UniTask<List<string>> GetAssetNamesByLabel(string label)
    {
        var locationsHandle = Addressables.LoadResourceLocationsAsync(label);
            
        var resourceLocations = await locationsHandle.ToUniTask();
        
        var names = resourceLocations
            .Select(location => location.PrimaryKey)
            .ToList();
        
        Addressables.Release(locationsHandle);
        return names;
    }
    
    /// <summary>
    /// Получение всех файлов указанного типа и указанного лейбла
    /// </summary>
    /// <param name="label"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async UniTask<List<string>> GetAssetNamesByLabel<T>(string label)
    {
        var locationsHandle =Addressables.LoadResourceLocationsAsync(label, typeof(T));
            
        var resourceLocations = await locationsHandle.ToUniTask();
        
        var names = resourceLocations
            .Select(location => location.PrimaryKey)
            .ToList();
        
        Addressables.Release(locationsHandle);
        return names;
    }
}
