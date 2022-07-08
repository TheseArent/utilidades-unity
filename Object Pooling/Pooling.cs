
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [Header("Configuracion")]
    public GameObject prefab;
    public int poolingSize;
    //
    public static Pooling manager;
    private List<GameObject> poolList = new List<GameObject>();
    private void Awake()
    {
        //nos auto-referenciamos en un estatico
        manager = this;
        //Creamos todos los prefabs para reutilizar
        for (int i = 0; i < poolingSize; i++)
        {
            GameObject prefabLocal = Instantiate(prefab, Vector2.zero, Quaternion.identity, this.transform);
            prefabLocal.SetActive(false);
            poolList.Add(prefabLocal);
        }
    }
    public GameObject Instantiate()
    {
        //Buscamos en la pool y devolvemos el primer objeto desactivado que encuentre
        for (int i = 0; i < poolingSize; i++)
        {
            if (!poolList[i].activeSelf)
            {
                poolList[i].SetActive(true);
                return poolList[i];
            }
        }
        Debug.LogError("[!]Todos los objetos de la pool estan activos , no se pueden reutilizar");
        return null;
    }
    public void AllPoolUnable()
    {
        //Desactivamos todos los objetos de la pool
        for (int i = 0; i < poolList.Count; i++)
        {
            poolList[i].SetActive(false);
        }
    }
}
