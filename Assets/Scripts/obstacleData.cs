using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class obstacleData : MonoBehaviour
{
    List<Vector3> pos = new List<Vector3>();
    public GameObject diamond;
    public class obstalceInfo
    {
        public Vector3 p1;
        public bool c1;
        public Vector3 p2;
        public bool c2;
        public Vector3 p3;
        public bool c3;
        public Vector3 p4;
        public bool c4;
        public Vector3 p5;
        public bool c5;
        public Vector3 p6;
        public bool c6;
        public Vector3 p7;
        public bool c7;
        public Vector3 p8;
        public bool c8;
        public Vector3 p9;
        public bool c9;
        public Vector3 p10;
        public bool c10;
        public Vector3 p11;
        public bool c11;
        public Vector3 p12;
        public bool c12;
        public Vector3 p13;
        public bool c13;
        public Vector3 p14;
        public bool c14;
        public Vector3 p15;
        public bool c15;
        public string correctShape;
    };
    
    private obstalceInfo Load(string name) {
        TextAsset file = Resources.Load(name.Substring(0, 12)) as TextAsset;
        if (file != null)
        {
            return JsonUtility.FromJson<obstalceInfo>(file.text);
        }
        else {
            return null;
        }
    }
    
    public string CreateColliders(GameObject obj)
    {
        obstalceInfo info = Load(obj.name);
        pos.Clear();
        print("Creating colliders...");
        if (info != null) {

            List<Vector3> positions = new List<Vector3>();
            positions.Add(info.p1);
            positions.Add(info.p2);
            positions.Add(info.p3);
            positions.Add(info.p4);
            positions.Add(info.p5);
            positions.Add(info.p6);
            positions.Add(info.p7);
            positions.Add(info.p8);
            positions.Add(info.p9);
            positions.Add(info.p10);
            positions.Add(info.p11);
            positions.Add(info.p12);
            positions.Add(info.p13);
            positions.Add(info.p14);
            positions.Add(info.p15);

            List<bool> isTrigger = new List<bool>();
            isTrigger.Add(info.c1);
            isTrigger.Add(info.c2);
            isTrigger.Add(info.c3);
            isTrigger.Add(info.c4);
            isTrigger.Add(info.c5);
            isTrigger.Add(info.c6);
            isTrigger.Add(info.c7);
            isTrigger.Add(info.c8);
            isTrigger.Add(info.c9);
            isTrigger.Add(info.c10);
            isTrigger.Add(info.c11);
            isTrigger.Add(info.c12);
            isTrigger.Add(info.c13);
            isTrigger.Add(info.c14);
            isTrigger.Add(info.c15);

            foreach (Vector3 v in positions) {
                GameObject empty = new GameObject();
                empty.name = (positions.IndexOf(v)+1).ToString();
                empty.transform.position = new Vector3(v.x, v.y, obj.transform.position.z);
                empty.transform.rotation = Quaternion.identity;
                BoxCollider bc = empty.AddComponent<BoxCollider>();
                bc.isTrigger = isTrigger[positions.IndexOf(v)];
                if (bc.isTrigger == true)
                {
                    pos.Add(empty.transform.position);
                }
                bc.size = Vector3.one * 0.95f;
                empty.AddComponent<obstacleTrigger>();
                empty.tag = "Obstacle";
                empty.transform.parent = obj.transform;
                //Vector3 diamondPos = new Vector3(Random.Range(-1, 2), 1f, 0f);
            }
            print("Finished creating colliders...");

            Vector3 diamondPos = pos[Random.Range(0,pos.Count)];
            diamondPos = new Vector3(Random.Range(-1, 2), 0.5f, diamondPos.z - 5f);
            GameObject temp = Instantiate(diamond, diamondPos,Quaternion.identity) as GameObject;
            temp.transform.parent = obj.transform;

            return info.correctShape;
        }
        return null;
    }
}
