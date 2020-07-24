using UnityEngine;

public class SolverAlgorithm : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        RunTestCases();
    }

    void RunTestCases() {
        string[] quantities = { "25 yd/s", "88 m/s", "Δt", "a", "54 m" };
        Debug.Log(KinematicSolver.SolveGeneral(quantities));
        Debug.Log(KinematicSolver.SolveGeneral(quantities));
    }
}
