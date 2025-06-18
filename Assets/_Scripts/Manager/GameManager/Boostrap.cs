using UnityEngine;


public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject[] singletonPrefabs;

    private void Awake()
    {
        foreach (var prefab in singletonPrefabs)
        {
            if (prefab == null) continue;
            var prefabName = prefab.name;

            if (GameObject.Find(prefabName + "(Clone)") == null && GameObject.Find(prefabName) == null)
            {
                Instantiate(prefab).name = prefabName;
            }
        }
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}
