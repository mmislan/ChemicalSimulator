using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject tempNodeStorage;
    public SimNode tempNodeComponentStorage;
    public SimNode[,,] nodeList;

    //public ChemicalReaction[] reactions;

    public float dX;
    public float dt;

    public int numXSlices;
    public int numYSlices;
    public int numZSlices;

    void Start()
    {
        dX = 1;
        dt = 1;
        numXSlices = 25;
        numYSlices = 25;
        numZSlices = 25;
        nodeList = new SimNode[numXSlices, numYSlices, numZSlices];
    }

    public void SpawnMeshCube()
    {

        for (int i = 0; i < numXSlices; i++)
        {
            for (int j = 0; j < numYSlices; j++)
            {
                for(int k = 0; k < numZSlices; k++)
                {
                    tempNodeStorage = Instantiate(nodePrefab, new Vector3(i * dX, j * dX, k*dX), Quaternion.identity);
                    tempNodeComponentStorage = tempNodeStorage.GetComponent<SimNode>();
                    nodeList[i, j, k] = tempNodeComponentStorage;
                }

            }
        }

    }

    //-------- Initializing the Square -------------

    /*
    public void InitializeSquareValues()
    {

        for (int i = 0; i < numXSlices; i++)
        {

            for (int j = 0; j < numYSlices; j++)
            {
                if (i == 0 || j == 0 || i == (numXSlices - 1) || j == (numYSlices - 1)) //Set Boundaries
                {
                    nodeList[i, j].SetBoundary();
                }
                else
                {
                    nodeList[i, j].InitializeConcentrations();
                    //nodeList[i, j].neighbourList[0] = nodeList[i - 1, j];
                    //nodeList[i, j].neighbourList[1] = nodeList[i + 1, j];
                    //nodeList[i, j].neighbourList[2] = nodeList[i, j-1];
                    //nodeList[i, j].neighbourList[3] = nodeList[i, j+1];


                }

            }
        }

    }

    //---------- Running Calculations -------
    public int numTimeSteps;

    public void RunCalculations()
    {
        numTimeSteps = 10;

        for (int i = 0; i < numTimeSteps; i++)
        {
            CalculateForwardDifferenceSquareValues();
            UpdateSquareValues();
        }
    }


    public void CalculateForwardDifferenceSquareValues()
    {
        for (int i = 0; i < numXSlices; i++)
        {
            for (int j = 0; j < numYSlices; j++)
            {
                if (!nodeList[i, j].isBoundary) //If the node is Not a Boundary
                {
                    //nodeList[i, j].CalcForwardDifferencesWithNeighbours();
                }
            }
        }
    }

    public void UpdateSquareValues()
    {
        for (int i = 0; i < numXSlices; i++)
        {
            for (int j = 0; j < numYSlices; j++)
            {
                if (!nodeList[i, j].isBoundary)
                {
                    nodeList[i, j].UpdateConcentrations();
                }
            }
        }
    }
    */

}
