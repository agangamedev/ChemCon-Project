using UnityEngine;

public class QuestionBank : MonoBehaviour
{
    public Question[] q;

    private void Awake()
    {
        if(!GameInstance.Instance.isDebug)
            Shuffle<Question>(q);

        SetAnswer3();
    }
    int totalElektron = 0;

    public void SetAnswer3()
    {/*
        for(int i=0; i<q.Length-1; i++)
        {
            string ans = "";
            totalElektron = q[i].NumAtom;

            int idx = 0;

            for(int k=0; k<6; k++)
            {
                string addAns = "";
                switch (idx)
                {
                    case 0: addAns = AddAns(addAns, 0, 2); break; // 1s2
                    case 1: addAns = AddAns(addAns, 0, 2); break; // 2s2
                    case 2: addAns = AddAns(addAns, 0, 6); break; // 2p6
                    case 3: addAns = AddAns(addAns, 0, 2); break; // 3s2
                    case 4: addAns = AddAns(addAns, 0, 6); break; // 3p6
                    case 5: addAns = AddAns(addAns, 0, 2); break; // 3p6
                }

                ans += addAns;
                idx++;
            }


            q[i].Answer._3 = ans;
        }
        */
    }

    private string AddAns(string addAns, int min, int max)
    {
        for (int j = 0; j < 10; j++)
        {
            Debug.Log("------------------------------ ");
            if (j >= min && j < max)
            {
                if(totalElektron > 0) //
                {
                    if(j%2 == 1) 
                    {
                        addAns += "1"; 
                        totalElektron -= 1;
                    }
                    else
                    {
                        if(totalElektron >= max / 2 && totalElektron - 1 >= 0)
                        {
                            addAns += "1";
                            totalElektron -= 1;
                        }
                        else
                        {
                            addAns += "0";
                        }
                    }
                }
                else
                {
                    addAns += "0";
                }
            }
            else
            {
                addAns += "0";
            }
        }

        return addAns;
    }

    public void ReShuffle()
    {
        Shuffle<Question>(q);
    }

    static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = Random.Range(0, i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}

[System.Serializable]
public class Question
{
    [SerializeField] private string nameAtom;
    [SerializeField] private int numAtom;
    [SerializeField] private string konfElektron;
    [SerializeField] private Answer answer;
    

    public string NameAtom { get => nameAtom; set => nameAtom = value; }
    public int NumAtom { get => numAtom; set => numAtom = value; }
    public Answer Answer { get => answer; set => answer = value; }
    public string KonfElektron { get => konfElektron; set => konfElektron = value; }

    public string GetQuest()
    {
        string q = "<sub>" + numAtom.ToString() + "</sub>" + nameAtom;
        return q;
    }
}

[System.Serializable]
public class Answer
{
    public string _1;  
    public string _2; 
    public string _3; 
    public string _4; 
    public string _5; 
}
