using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text m_WaveCounter;
    private int m_wave = 1;
    private int m_MaxNumOfWaves;

    [SerializeField]
    private Text m_TowerHealthText;
    private int m_TowerHealth = 10;

    [SerializeField]
    private Text m_GameOverPrompt;

    [SerializeField]
    private Text m_GameWonPrompt;

    private void Start()
    {
        m_WaveCounter.text = $"Wave: {m_wave}";
        m_TowerHealthText.text = $"Tower health: {m_TowerHealth}";
        m_GameOverPrompt.gameObject.SetActive(false);
        m_GameWonPrompt.gameObject.SetActive(false);

        print(m_MaxNumOfWaves);
    }

    public void UpdateTowerHealth()
    {
        m_TowerHealth--;
        if (m_TowerHealth <= 0)
        {
            m_GameOverPrompt.gameObject.SetActive(true);
        }
    }

    public void UpdateWaveUI()
    {
        if (m_MaxNumOfWaves == m_wave ) //Check if there are enemies left
        {
            m_WaveCounter.text = "Final Wave";

        }
        else
        {
            m_wave++;
        }

        m_WaveCounter.text = $"Wave: {m_wave}";
    }

    public void GetMaxWave(int max)
    {
        m_MaxNumOfWaves = max;
    }
}