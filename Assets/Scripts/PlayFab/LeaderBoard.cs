using PlayFab;
using PlayFab.ServerModels;
using UnityEngine;
using System.Collections.Generic;

public class LeaderBoard : MonoBehaviour
{
    public ListMembers listMembers;
    public Sprite menAvatar;
    public Sprite girlAvatar;
    
        // Start is called before the first frame update
    void Start()
    {
        
        //GetLeaderBoard();

        




    }

    public void GetLeaderBoard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest();
        request.MaxResultsCount = 10;
        request.StatisticName = "Gold";
        PlayerProfileViewConstraints playerProfile = new PlayerProfileViewConstraints();
        playerProfile.ShowStatistics = true;
        playerProfile.ShowDisplayName = true;
        request.ProfileConstraints = playerProfile;
        PlayFabServerAPI.GetLeaderboard(request, OnResultLeaderboard, error => { Debug.Log("Error"); });

    }

    public void OnResultLeaderboard(GetLeaderboardResult result)
    {
        int i = 0;
        var playersLeaderBoard = listMembers.membersList;
        playersLeaderBoard.Clear();

        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            ListMembers.MembersList newObject = new ListMembers.MembersList();
            newObject.type = ListMembers.Selection.Ranking;
            playersLeaderBoard.Add(newObject);
            playersLeaderBoard[i].Login = player.DisplayName;
            playersLeaderBoard[i].Gain = player.StatValue.ToString();
            playersLeaderBoard[i].rank = i + 1;
            
            List<StatisticModel> playerModel = player.Profile.Statistics;
            foreach (StatisticModel statistic in playerModel)
            {
                if (statistic.Name == "Gender")
                {
                    if (statistic.Value == 1)
                    {
                        playersLeaderBoard[i].avatar = menAvatar;
                    }
                    if (statistic.Value == 2)
                    {
                        playersLeaderBoard[i].avatar = girlAvatar;
                    }
                }

                if (statistic.Name == "Wins")
                {
                    playersLeaderBoard[i].totalWins = statistic.Value;
                }
                if (statistic.Name == "ParentCoins")
                {
                    playersLeaderBoard[i].coinsParent = statistic.Value.ToString();
                }

                if (statistic.Name == "ChildCoins")
                {
                    playersLeaderBoard[i].coinsChild = statistic.Value.ToString();
                }


             }
            i++;            
        }

        listMembers.SetLeaderBoard();
        PlayFabOrder.instance.isGetLeaderBoard = true;
        if (MenuManager.menuManager.isFinishLevel)
        {
            UI.instance.animBottom_Button(4);
            MenuManager.menuManager.isFinishLevel = false;
        }

    }

       





}
