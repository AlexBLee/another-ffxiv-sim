using System.IO;
using UnityEngine;

[System.Serializable]
public class Waymarker
{
    public float X;
    public float Y;
    public float Z;
    public int ID;
    public bool Active;
}

// Would love for another way to parse it without making an object for every symbol,
// but that's just how they decided to format the json.
[System.Serializable]
public class WaymarkerData
{
    public string Name;
    public int MapID;
    public Waymarker A;
    public Waymarker B;
    public Waymarker C;
    public Waymarker D;
    public Waymarker One;
    public Waymarker Two;
    public Waymarker Three;
    public Waymarker Four;
}

public class WayMarkerSpawner : MonoBehaviour
{
    [SerializeField, TextArea(1,20)] private string _wayMarkerJson;

    [SerializeField] private GameObject _wayMarkerPrefab_A;
    [SerializeField] private GameObject _wayMarkerPrefab_B;
    [SerializeField] private GameObject _wayMarkerPrefab_C;
    [SerializeField] private GameObject _wayMarkerPrefab_D;

    [SerializeField] private GameObject _wayMarkerPrefab_1;
    [SerializeField] private GameObject _wayMarkerPrefab_2;
    [SerializeField] private GameObject _wayMarkerPrefab_3;
    [SerializeField] private GameObject _wayMarkerPrefab_4;

    private void Start()
    {
        string json = _wayMarkerJson;

        WaymarkerData waymarkerData = JsonUtility.FromJson<WaymarkerData>(json);

        if (waymarkerData.A.Active) Spawn(_wayMarkerPrefab_A, waymarkerData.A);
        if (waymarkerData.B.Active) Spawn(_wayMarkerPrefab_B, waymarkerData.B);
        if (waymarkerData.C.Active) Spawn(_wayMarkerPrefab_C, waymarkerData.C);
        if (waymarkerData.D.Active) Spawn(_wayMarkerPrefab_D, waymarkerData.D);

        if (waymarkerData.One.Active) Spawn(_wayMarkerPrefab_1, waymarkerData.One);
        if (waymarkerData.Two.Active) Spawn(_wayMarkerPrefab_2, waymarkerData.Two);
        if (waymarkerData.Three.Active) Spawn(_wayMarkerPrefab_3, waymarkerData.Three);
        if (waymarkerData.Four.Active) Spawn(_wayMarkerPrefab_4, waymarkerData.Four);
    }

    private void Spawn(GameObject prefab, Waymarker waymarker)
    {
        var waymarkerObject = Instantiate(original: prefab,  parent: transform);

        var xOffset = 100;
        var zOffset = 110;

        Vector3 waymarkerPosition = new Vector3(waymarker.X - xOffset, waymarker.Y, waymarker.Z - zOffset);
        waymarkerObject.transform.localPosition = waymarkerPosition;
    }
}
