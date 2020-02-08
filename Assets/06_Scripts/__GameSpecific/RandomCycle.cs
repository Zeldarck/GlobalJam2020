using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCycle
{

    int m_currentIndex;
    int m_cycleThreshold;
    List<int> m_randomList = new List<int>();

    public RandomCycle(int a_size, int a_cyleThreshold)
    {
        m_cycleThreshold = a_cyleThreshold;
        for (int i = 0; i < a_size; ++i)
        {
            m_randomList.Add(i);
        }
        ResetRandomList();

    }


    void ResetRandomList()
    {
        Utils.ShuffleList(m_randomList);
        m_currentIndex = 0;
    }

    public int GetNextRandom()
    {

        if(m_currentIndex >= m_cycleThreshold || m_currentIndex >= m_randomList.Count)
        {
            ResetRandomList();
        }

        int res = m_randomList[m_currentIndex];

        ++m_currentIndex;

        return res;
    }

}
