using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SunUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sunText;

    private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        UpdateSunText();

        SunManager.OnSunCollected += UpdateSunText;
    }

    private void OnDestroy()
    {
        SunManager.OnSunCollected -= UpdateSunText;
    }

    private void UpdateSunText()
    {
        GameManager.Instance.saveData.sunCountPerLevel.TryGetValue(sceneName, out int count);
        sunText.text = $": {count} / 3";
    }
}