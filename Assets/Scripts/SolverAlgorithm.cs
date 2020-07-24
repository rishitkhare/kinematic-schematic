using UnityEngine;

public class SolverAlgorithm : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        RunTestCases();
    }

    void RunTestCases() {
        string[] quantities = { "2", "4", "Δt", "1", "ΔX" };
        Debug.Log(KinematicSolver.SolveEarth(quantities));
        //Debug.Log(KinematicSolver.SolveGeneral(quantities));
    }
}
