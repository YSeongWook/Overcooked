using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] countdown;
    [SerializeField] private Transform[] playerSpawnPositions; // 리스폰 위치들
    [SerializeField] private GameObject canvas;
    [SerializeField] private float respawnDelay = 5.0f; // 리스폰 대기 시간

    private void Start()
    {
        if (countdown.Length != playerSpawnPositions.Length)
        {
            Debug.LogError("CountdownTexts와 PlayerSpawnPositions 배열의 크기가 일치해야 합니다.");
            return;
        }

        // 초기에는 모든 UI를 비활성화
        foreach (var obj in countdown)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void StartRespawnCountdown(GameObject player)
    {
        int spawnIndex = GetSpawnIndexFromParentName(player.transform.name);

        if (spawnIndex >= 0 && spawnIndex < countdown.Length)
        {
            StartCoroutine(RespawnCountdownCoroutine(player, spawnIndex));
        }
        else
        {
            Debug.LogError("잘못된 spawnIndex입니다.");
        }
    }

    private int GetSpawnIndexFromParentName(string parentName)
    {
        if (parentName.Contains("1"))
        {
            return 0;
        }
        else if (parentName.Contains("2"))
        {
            return 1;
        }
        else if (parentName.Contains("3"))
        {
            return 2;
        }
        else if (parentName.Contains("4"))
        {
            return 3;
        }
        else
        {
            return -1; // 숫자가 없으면 -1 반환
        }
    }

    private IEnumerator RespawnCountdownCoroutine(GameObject player, int index)
    {
        Text countdownText = countdown[index].transform.GetChild(1).GetComponent<Text>();
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(playerSpawnPositions[index].position);

        countdown[index].transform.position = screenPosition;
        countdown[index].SetActive(true);

        for (int i = (int)respawnDelay; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        countdown[index].SetActive(false);
        RespawnPlayer(player, index);
    }

    private void RespawnPlayer(GameObject player, int index)
    {
        Transform spawnPosition = playerSpawnPositions[index];
        Debug.Log($"플레이어가 {spawnPosition.position}에서 리스폰되었습니다.");

        // 연기 발생 및 플레이어 스폰
        PlayerPuff.Instance.SpawnPuff(spawnPosition);
        player.transform.position = spawnPosition.position;

        PlayerInteractController playerInteractController = player.GetComponent<PlayerInteractController>();
        Player2InteractController player2InteractController = player.GetComponent<Player2InteractController>();
        Animator playerAnimator = player.transform.GetChild(0).GetChild(1).GetComponent<Animator>();

        if(playerInteractController != null)
        {
            playerInteractController.IsHolding = false;
            playerInteractController.CanActive = false;
        } else
        {
            player2InteractController.IsHolding = false;
            player2InteractController.CanActive = false;
        }

        playerAnimator.SetBool("isHolding", false);

        // 아래 방향을 바라보도록 설정
        player.transform.rotation = Quaternion.Euler(0, 180, 0);

        // 연기가 모두 나오고 스폰해야 하기에 1초 딜레이
        player.transform.GetChild(0).gameObject.SetActive(true);
    }
}
