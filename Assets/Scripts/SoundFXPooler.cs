using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundFXPooler : MonoBehaviour
{
    public static SoundFXPooler pooling;
    public int ManyPool;
    public bool NeedMore;
    public GameObject Pools;
    public List<GameObject> poolobjects;

    private void Awake()
    {
        pooling = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolobjects = new List<GameObject>();
        for (int i = 0; i < ManyPool; i++)
        {
            GameObject obj = Instantiate(Pools);
            obj.SetActive(false);
            poolobjects.Add(obj);

        }

    }
    public GameObject GetPool()
    {
        for (int i = 0; i < poolobjects.Count; i++)
        {
            if (!poolobjects[i].activeInHierarchy)
            {
                return poolobjects[i];
            }
        }

        if (NeedMore)
        {
            GameObject obj = Instantiate(Pools);
            poolobjects.Add(obj);
            return obj;
        }

        return null;
    }
    // Update is called once per frame
    void Update()
    {

    }


}
