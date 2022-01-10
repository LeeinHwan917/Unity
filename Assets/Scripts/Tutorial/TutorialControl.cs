using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialControl : MonoBehaviour
{
    [SerializeField]
    private Text tutorialText;
    [SerializeField]
    private GameObject skipText;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform shootingrangePos;


    void Start()
    {
        tutorialText.text = "Ʃ�丮�� ���Ű��� ȯ���մϴ�!";
        skipText.SetActive(true);

        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "W, A, S, D Ű�� ������ �̵��� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "Shift Ű�� ���� �޸� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "Space Ű�� ���� ������ �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "��������� �̵��մϴ�...");
        yield return new WaitForSeconds(0.5f);
        GoShootingRange();

        SetText(tutorialText, "���콺 ��Ŭ���� ���� ����� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "R Ű�� ���� �������� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "�ѱ⸦ ���� EŰ�� ������ ���� �ٲ� �� �ֽ��ϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "FŰ�� ������ �������� ����մϴ�.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "Ʃ�丮���� ������� �Դϴ�!");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "3�� �� Ÿ��Ʋ�� ���ư��ϴ�...");
        yield return new WaitForSeconds(3.0f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    private void GoShootingRange()
    {
        player.transform.position = shootingrangePos.transform.position;
    }

    private void SetText(Text text, string str)
    {
        text.text = str;
    }
}
