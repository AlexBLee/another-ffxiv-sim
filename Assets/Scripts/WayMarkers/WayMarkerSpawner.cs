using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class WaymarkerData
{
    public float X;
    public float Y;
    public float Z;
}

public class WayMarkerSpawner : MonoBehaviour
{
    [SerializeField, TextArea(1,20)] private string _wayMarkerJson;
    [SerializeField] private Waymarker _waymarker;

    private Dictionary<string, WaymarkerData> _waymarkers = new();

    private readonly Dictionary<string, int> _nameToNumber = new()
    {
        { "One", 1 },
        { "Two", 2 },
        { "Three", 3 },
        { "Four", 4 },
    };

    private void Start()
    {
        InitializeJsonData(_wayMarkerJson);
        SpawnWaymarkers();
    }

    private void InitializeJsonData(string json)
    {
        var root = JObject.Parse(json);

        foreach (var prop in root.Properties())
        {
            if (prop.Name == "Name" || prop.Name == "MapID")
                continue;

            var dataName = prop.Name;

            if (_nameToNumber.TryGetValue(prop.Name, out int number))
                dataName = number.ToString();

            WaymarkerData waymarker = prop.Value.ToObject<WaymarkerData>();
            _waymarkers[dataName] = waymarker;
        }
    }

    private void SpawnWaymarkers()
    {
        foreach (var waymarker in _waymarkers)
        {
            Spawn(waymarker.Key, waymarker.Value);
        }
    }

    private void Spawn(string key, WaymarkerData waymarker)
    {
        var waymarkerObject = Instantiate(original: _waymarker,  parent: transform);
        waymarkerObject.SetData(key, waymarker);
    }
}
