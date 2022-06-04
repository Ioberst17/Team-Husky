using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRankData : MonoBehaviour
{
    // Used to rank player times and assist with rewards / referenced in PlayerController
    // Notes: Should be attached to PlayerModel in levels
    public GameManager gameManager;
    public class RankAwardAndEXPData
    {
        public float levelTime;
        public string rank;
        public int award;
        public float exp;
    }

    public int currentLevel;

    // Level Ranking Information
    public Dictionary<string, RankAwardAndEXPData> levelRanking;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        levelRanking = populateLevelInformation(gameManager.LevelNumberChecker());
        currentLevel = gameManager.LevelNumberChecker(); // used for debug
    }

    public Dictionary<string, RankAwardAndEXPData> populateLevelInformation(int level)
    {
        switch (level)
        {
            case 1:
                levelRanking = new Dictionary<string, RankAwardAndEXPData>
                {
                    {"Diamond", new RankAwardAndEXPData{levelTime=45F, rank="Diamond", award=50, exp=100 } },
                    {"Gold", new RankAwardAndEXPData{levelTime=60F, rank="Gold", award=40, exp=50} },
                    {"Silver", new RankAwardAndEXPData{levelTime=80F, rank="Silver", award=30, exp=20} },
                    {"Bronze", new RankAwardAndEXPData{levelTime=80F, rank="Bronze", award=20, exp=10} }
                };
                break;
            case 2:
                levelRanking = new Dictionary<string, RankAwardAndEXPData>
                {
                    {"Diamond", new RankAwardAndEXPData{levelTime=45F, rank="Diamond", award=50, exp=150 } },
                    {"Gold", new RankAwardAndEXPData{levelTime=60F, rank="Gold", award=40, exp=75} },
                    {"Silver", new RankAwardAndEXPData{levelTime=80F, rank="Silver", award=30, exp=40} },
                    {"Bronze", new RankAwardAndEXPData{levelTime=80F, rank="Bronze", award=20, exp=20} }
                };
                break;
            case 3:
                levelRanking = new Dictionary<string, RankAwardAndEXPData>
                {
                    {"Diamond", new RankAwardAndEXPData{levelTime=45F, rank="Diamond", award=50, exp=200 } },
                    {"Gold", new RankAwardAndEXPData{levelTime=60F, rank="Gold", award=40, exp=100} },
                    {"Silver", new RankAwardAndEXPData{levelTime=80F, rank="Silver", award=30, exp=50} },
                    {"Bronze", new RankAwardAndEXPData{levelTime=80F, rank="Bronze", award=20, exp=25} }
                };
                break;
        }

        return levelRanking;
    }

}
