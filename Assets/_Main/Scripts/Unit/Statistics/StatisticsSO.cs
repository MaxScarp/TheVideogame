using UnityEngine;
using static StatisticsShared;

[CreateAssetMenu(menuName = "ScriptableObjects/Statistic")]
public class StatisticsSO : ScriptableObject
{
    public StatisticName Name;
    public int Level;
    [Space(10)]
    public SubStatistic SubStat1; 
    public SubStatistic SubStat2;
    public SubStatistic SharedSubStat1;
    public SubStatistic SharedSubStat2;
}