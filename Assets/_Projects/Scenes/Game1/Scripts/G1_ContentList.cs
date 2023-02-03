using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class G1_ContentList : MonoBehaviour
{
    [SerializeField] private List<NodeParents> nodeParent = new List<NodeParents>();

    [SerializeField] private GameObject prefNode;

    private Question quest;
    [SerializeField]private List<ShellConfig> elecConf = new List<ShellConfig>();

    int nodeRow = 0;
    int nodeColumn = 0;

    #region Initialize SubShell

    private List<int> subshell_S = new List<int>()
    {
        1, 2, 4, 6, 9, 12, 16, 20
    };

    private List<int> subshell_P = new List<int>()
    {
        3, 5, 8, 11, 15, 19
    };

    private List<int> subshell_D = new List<int>()
    {
        7, 10, 14, 18
    };

    private List<int> subshell_F = new List<int>()
    {
        13, 17
    };

    #endregion


    private void Start()
    {
        /* Setup Variable*/

        int qID = GameInstance.Instance.QuestionID;

        QuestionBank qBank = FindObjectOfType<QuestionBank>();
        if (qBank != null)
            quest = qBank.q[qID];

        nodeRow = nodeParent.Count;
        nodeColumn = nodeParent[0].nodeColumn.Count;

        // Spawning Answer Node

        //SpawningRightNodes();

        DefineElecConf();

        /* Spawning Random Node */

        SpawnRandomNode(nodeRow, nodeColumn);
            
    }

    #region Answer Node

    private void DefineElecConf()
    {
        int numAtom = quest.NumAtom;
        int iteration = 1;

        SubshellEnum eLastShell = SubshellEnum.S;
        int tempElec = numAtom;
        int tempNumb = 0;

        while (numAtom > 0)
        {
            SubshellEnum tempShell = SubshellEnum.S;

            tempElec = numAtom;

            if (subshell_S.Contains(iteration))
            {
                tempShell = SubshellEnum.S;
                if (numAtom >= 2) { tempElec = 2; }

                SetNumShell(ref eLastShell, ref tempNumb, tempShell);
            }
            else if (subshell_P.Contains(iteration))
            {
                tempShell = SubshellEnum.P;
                if (numAtom >= 6) { tempElec = 6; }

                SetNumShell(ref eLastShell, ref tempNumb, tempShell);
            }
            else if (subshell_D.Contains(iteration))
            {
                tempShell = SubshellEnum.D;
                if (numAtom >= 10) { tempElec = 10; }

                SetNumShell(ref eLastShell, ref tempNumb, tempShell);
            }
            else if (subshell_F.Contains(iteration))
            {
                tempShell = SubshellEnum.F;
                if (numAtom >= 14) { tempElec = 14; }

                SetNumShell(ref eLastShell, ref tempNumb, tempShell);
            }

            //string newConf = tempNumb + tempShell.ToString() + "<sup>" + tempElec + "</sup>";

            ShellConfig shell = new ShellConfig(tempNumb,tempShell,tempElec);

            elecConf.Add(shell);

            iteration++;
            numAtom -= tempElec;

        }

        SpawningRightNodes();
    }



    private void SpawningRightNodes()
    {
        int rRow = Random.Range(0, nodeRow - 1);
        int rCol = Random.Range(0, nodeColumn - 1);

        List<GameObject> answerNode = new List<GameObject>(); // answerNode
        List<string> tempAnswerIndex = new List<string>(); // temporary Node for 9 chance next node
        List<string> tempNodeIndex = new List<string>(); // temporary Node for 9 chance next node

        answerNode.Add(nodeParent[rRow].nodeColumn[rCol]);
        tempAnswerIndex.Add(rRow + "" + rCol);

        for (int i=0; i<elecConf.Count-1; i++)
        {


            // define next answerNode 
            for(int j=-1; j <=1; j++) // looping for 3 chance next node
            {
                for(int k=-1; k<=1; k++) // looping for 3 chance next node
                {
                    if(rRow + j >= 0 && rRow + j < nodeRow) // make sure node in the grid row
                    {
                        if(rCol + k >= 0 && rCol + k < nodeColumn)  // make sure node in the grid column
                        {
                            string checkNode = (rRow + j).ToString() + "" + (rCol + k).ToString();
                            
                            //Debug.Log("Check String " + checkNode + " is contain " + tempAnswerIndex.Contains(checkNode));
                            if (!tempAnswerIndex.Contains(checkNode))
                            {
                                tempNodeIndex.Add((rRow + j).ToString() + (rCol + k).ToString());
                            }
                        }
                    }
                }
            }

            // Add One Random tempNode to answerNode

            int ranNode = Random.Range(0, tempNodeIndex.Count);

            string cR = tempNodeIndex[ranNode];

            rRow = int.Parse(cR[0].ToString());
            rCol = int.Parse(cR[1].ToString());
            
            tempNodeIndex.Clear();

            answerNode.Add(nodeParent[rRow].nodeColumn[rCol]);
            tempAnswerIndex.Add(rRow + "" + rCol);
        }

        for(int i=0; i<answerNode.Count; i++)
        {
            GameObject parent = answerNode[i];

            if (answerNode[i].transform.childCount <= 0)
            {
                
                ShellConfig shellConfig = elecConf[i];
                GameObject newNode = Instantiate(prefNode, parent.transform);
                newNode.GetComponent<G1_Node>().SetSubShell(shellConfig.IndexShell, shellConfig.SubShell, shellConfig.ElectronShell);
                //newNode.GetComponent<G1_Node>().DebugAnswer();

            }
            else
            {
                Debug.Log("Parent isn't empty");
            }
        }
    }

    private static void SetNumShell(ref SubshellEnum lastShell, ref int tempNumb, SubshellEnum tempShell)
    {
        if (lastShell == tempShell)
        {
            tempNumb++;
        }
        else
        {
            if (tempShell == SubshellEnum.P)
            {
                if (lastShell == SubshellEnum.D)
                {
                    tempNumb += 1;
                }
            }
            else if (tempShell == SubshellEnum.D)
            {
                if (lastShell == SubshellEnum.S)
                {
                    tempNumb -= 1;
                }
                else if (lastShell == SubshellEnum.F)
                {
                    tempNumb += 1;
                }
            }
            else if (tempShell == SubshellEnum.F)
            {
                if (lastShell == SubshellEnum.S)
                {
                    tempNumb -= 2;
                }
            }
            else if (tempShell == SubshellEnum.S)
            {
                tempNumb += 1;
            }
        }
        lastShell = tempShell;
    }

    #endregion

    private void SpawnRandomNode(int nodeRow, int nodeColumn)
    {
        for (int i = 0; i < nodeRow; i++)
        {
            for (int j = 0; j < nodeColumn; j++)
            {
                GameObject parent = nodeParent[i].nodeColumn[j];

                if(parent)
                {
                    if (parent.transform.childCount <= 0)
                    {
                        GameObject newNode = Instantiate(prefNode, parent.transform);
                        newNode.GetComponent<G1_Node>().RandomNode();
                    }
                }
                else{
                    Debug.Log("Parent null on coordinate " + i + "," + j);
                }
                
            }
        }
    }

}

[System.Serializable]
public class NodeParents
{
    public List<GameObject> nodeColumn = new List<GameObject>(5);
}

public enum SubshellEnum
{
    S, P, D, F
}

public class ShellConfig{

    public ShellConfig(int indexShell, SubshellEnum subShell, int electronShell)
    {
        this.IndexShell = indexShell;
        this.SubShell = subShell;
        this.ElectronShell = electronShell;
    }

    private int indexShell;
    private SubshellEnum subShell;
    private int electronShell;

    public int IndexShell { get => indexShell; set => indexShell = value; }
    public SubshellEnum SubShell { get => subShell; set => subShell = value; }
    public int ElectronShell { get => electronShell; set => electronShell = value; }
}