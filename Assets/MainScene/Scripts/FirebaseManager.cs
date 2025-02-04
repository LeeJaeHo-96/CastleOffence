using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    static public FirebaseManager instance;

    DatabaseReference databaseReference;

    int listNum;
    void Awake()
    {
        Init();
    }

    public void SaveScore(string userId, int score)
    {
        // userId/scores�� ���� ����
        databaseReference.Child("scores").Child(userId).SetValueAsync(score)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("���� ���� ����!");
                    Debug.Log((userId,score));
                }
            });
    }

    public void LoadLeaderboard(List<TMP_Text> rankList)
    {
        //����� �������� 5���� �߷����� ����Ʈ�� �־���
        databaseReference.Child("scores").OrderByChild("score").LimitToLast(5).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("�������� �ҷ����� ����!");
                DataSnapshot snapshot = task.Result;

                List<string> leaderboardEntries = new List<string>();

                foreach (DataSnapshot player in snapshot.Children)
                {
                    string playerName = player.Key;
                    int score = int.Parse(player.Value.ToString());
                    leaderboardEntries.Add($"�̸�: {playerName} /{score}��");
                }

                // ������ ����Ʈ�� ����� ���� ������ ���� ���� ����
                leaderboardEntries.Reverse();

                //���⼭ ���������� ����Ʈ�� �־���
                for (int i = 0; i < leaderboardEntries.Count && i < rankList.Count; i++)
                {
                    rankList[i].text = leaderboardEntries[i];
                }
            }
        });
    }

    void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });

        listNum = 0;
    }
}
