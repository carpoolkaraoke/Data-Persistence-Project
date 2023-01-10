using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{

    public TextMeshProUGUI nameText;

    int maxNameLength = 10;

    private void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                if (nameText.text.Length > 0)
                {
                    nameText.text = nameText.text.Substring(0, nameText.text.Length - 1);
                }
            }
            else
            {
                if (nameText.text.Length < maxNameLength)
                {
                    nameText.text = nameText.text + c;
                }
            }
        }

        MainManager.Instance.playerName = nameText.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        MainManager.Instance.SaveDataToFile();

#if UNITY_EDITOR

        UnityEditor.EditorApplication.ExitPlaymode();

        return;

#endif

        Application.Quit();
    }
}
