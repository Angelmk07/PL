using _Source.ObjectScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Source.Core
{
    public class GeneratorLvl : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Easy = new List<GameObject>();
        [SerializeField] private List<GameObject> Medium = new List<GameObject>();
        [SerializeField] private List<GameObject> Hard = new List<GameObject>();

        private List<GameObject> Easypool = new List<GameObject>();
        private List<GameObject> Mediumpool = new List<GameObject>();
        private List<GameObject> Hardpool = new List<GameObject>();

        [SerializeField] private GameObject finish;
        [SerializeField] private Vector3 distance;
        [field: SerializeField] public GameObject startposition { get; private set; }

        [SerializeField] private int obstacles = 2;
        private int startChance = 80;
        private int maxChance = 100;
        private int easyStep = 5;
        private int mediumStep = 2;
        private int hardStep = 3;
        private int dublicates = 2;
        private GeneratorLvl generator;

        private void Start()
        {
            generator = GetComponent<GeneratorLvl>();
            InitializePools();
            GenerateLvl();
        }
        void InitializePools()
        {
            foreach (var obj in Easy)
            {
                for (int i = 0; i < dublicates; i++)
                {
                    var instance = Instantiate(obj);
                    instance.AddComponent<LvlReturn>().Construct(ref Easypool, generator);
                    instance.SetActive(false);
                    Easypool.Add(instance);

                }
            }
            foreach (var obj in Medium)
            {
                for (int i = 0; i < dublicates; i++)
                {
                    var instance = Instantiate(obj);
                    instance.AddComponent<LvlReturn>().Construct(ref Mediumpool, generator);
                    instance.SetActive(false);
                    Mediumpool.Add(instance);
                }
            }
            foreach (var obj in Hard)
            {
                for (int i = 0; i < dublicates; i++)
                {
                    var instance = Instantiate(obj);
                    instance.AddComponent<LvlReturn>().Construct(ref Hardpool, generator);
                    instance.SetActive(false);
                    Hardpool.Add(instance);
                }
            }
        }
        void GenerateLvl()
        {
            for (int i = 0; i < obstacles; i++)
            {
                GameObject currentObj = null;
                int random = Random.Range(0, maxChance);
                int easyChance = Mathf.Clamp(startChance - easyStep * i, 0, maxChance);
                int mediumChance = Mathf.Clamp(easyChance + mediumStep * i, 0, maxChance);
                int hardChance = Mathf.Clamp(mediumChance + hardStep * i, 0, maxChance);

                if (random < easyChance)
                {
                    currentObj = GetOrCreateObjectFromPool(Easypool, Easy);
                }
                else if (random < mediumChance)
                {
                    currentObj = GetOrCreateObjectFromPool(Mediumpool, Medium);
                }
                else if (random < hardChance)
                {
                    currentObj = GetOrCreateObjectFromPool(Hardpool, Hard);
                }
                if (currentObj != null)
                {
                    currentObj.transform.position = startposition.transform.position;
                    startposition.transform.position += distance;
                }

            }
            StartCoroutine(SetFinish());
        }

        private GameObject GetOrCreateObjectFromPool(List<GameObject> pool, List<GameObject> sourceList)
        {
            if (pool.Count > 0)
            {
                var obj = pool[0];
                pool.RemoveAt(0);
                obj.SetActive(true);
                return obj;
            }
            else
            {
                var prefab = sourceList[Random.Range(0, sourceList.Count)];
                var instance = Instantiate(prefab);
                instance.AddComponent<LvlReturn>().Construct(ref pool, generator);
                instance.SetActive(true);
                return instance;
            }
        }

        public void ReturnObjectToPool(List<GameObject> pool, GameObject obj)
        {
            obj.SetActive(false);
            pool.Add(obj);
        }

        public void FinishEvent(GameObject point)
        {
            obstacles++;
            startposition.transform.position = new Vector3(point.transform.position.x + 6, 0, 0);
            GenerateLvl();
        }

        IEnumerator SetFinish()
        {
            yield return new WaitForSeconds(6.5f);
            finish.transform.position = startposition.transform.position;
            startposition.transform.position += distance;
        }
    }
}