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
        tutorialText.text = "튜토리얼에 오신것을 환영합니다!";
        skipText.SetActive(true);

        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "W, A, S, D 키를 눌러서 이동할 수 있습니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "Shift 키를 눌러 달릴 수 있습니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "Space 키를 눌러 점프할 수 있습니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "사격장으로 이동합니다...");
        yield return new WaitForSeconds(0.5f);
        GoShootingRange();

        SetText(tutorialText, "마우스 좌클릭을 눌러 사격할 수 있습니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "R 키를 눌러 재장전할 수 있습니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "총기를 보고 E키를 누르면 총을 바꿀 수 있습니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "F키를 누르면 아이템을 사용합니다.");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "튜토리얼은 여기까지 입니다!");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Return) != true);

        SetText(tutorialText, "3초 뒤 타이틀로 돌아갑니다...");
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
