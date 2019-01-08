using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphACentauryR
{
    public class HexMapManager : MonoBehaviour
    {

        public Material[] hexMaterials;
        public GameObject HexPrefab;
        public GameObject Hex;


        public int NumRows = 60;
        public int NumColumns = 120;

        // Use this for initialization
        void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            for (int column = 0; column < 10; column++)
            {
                for (int row = 0; row < 10; row++)
                {
                    Instantiate(HexPrefab, new Vector3(column, 0, row), Quaternion.identity, this.transform);
                }
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
