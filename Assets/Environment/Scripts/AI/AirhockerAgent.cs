using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine;


public class AirhockerAgent : Agent
{
    [HideInInspector] public Controller controller;
    private AirhockerTrainer _airhockerTrainer;
    [HideInInspector] public Rigidbody rb;
    private Vector3 _initialLocalPosition;
    private Vector3 _initialWasherLocalPosition;
    private BehaviorParameters _behaviorParameters;

    [HideInInspector] public int score = 0;
    public Rigidbody rbWasher;
    public Rigidbody rbEnemy;
    public Transform gate;
    public Transform enemyGate;


    private void Start()
    {
        _airhockerTrainer = GetComponentInParent<AirhockerTrainer>();
        _behaviorParameters = GetComponent<BehaviorParameters>();
        rb = GetComponentInChildren<Rigidbody>();
        _initialLocalPosition = rb.transform.localPosition;
        _initialWasherLocalPosition = rbWasher.transform.localPosition;
        controller = GetComponent<Controller>();
    }

    public void ResetGame()
    {
        rb.transform.localPosition = _initialLocalPosition;
        rb.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rbWasher.transform.localPosition = _initialWasherLocalPosition;
        rbWasher.velocity = Vector3.zero;
        rbWasher.angularVelocity = Vector3.zero;
    }

    public override void OnEpisodeBegin()
    {
        ResetGame();
        _airhockerTrainer.ResetGame("");
        score = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 4
        sensor.AddObservation(gate.localPosition.x);
        sensor.AddObservation(gate.localPosition.z);
        sensor.AddObservation(enemyGate.localPosition.x);
        sensor.AddObservation(enemyGate.localPosition.z);
        
        // 5
        sensor.AddObservation(rb.transform.localPosition.x);
        sensor.AddObservation(rb.transform.localPosition.z);
        sensor.AddObservation(rb.transform.rotation.y);
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);

        // 5
        sensor.AddObservation(rbEnemy.transform.localPosition.x);
        sensor.AddObservation(rbEnemy.transform.localPosition.z);
        sensor.AddObservation(rbEnemy.transform.rotation.y);
        sensor.AddObservation(rbEnemy.velocity.x);
        sensor.AddObservation(rbEnemy.velocity.z);
        
        // 6
        sensor.AddObservation(rbWasher.transform.localPosition.x);
        sensor.AddObservation(rbWasher.transform.localPosition.z);
        sensor.AddObservation(rbWasher.velocity.x);
        sensor.AddObservation(rbWasher.velocity.z);
        sensor.AddObservation(Vector3.Distance(rb.transform.localPosition, rbWasher.transform.localPosition));
        sensor.AddObservation(Vector3.Distance(rbEnemy.transform.localPosition, rbWasher.transform.localPosition));
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        for (int i = 0; i < actions.DiscreteActions.Length; i++)
        {
            if (actions.DiscreteActions[i] == 1)
                controller.MoveStick(i);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        var isRed = CompareTag("playerRed");
        var isBlue = CompareTag("playerBlue");

        if (Input.GetKey(KeyCode.W) && isBlue || Input.GetKey(KeyCode.UpArrow) && isRed)
            if (Mathf.Abs(rb.velocity.x) < controller.maxSpeed.x)
                discreteActionsOut[3] = 1;

        if (Input.GetKey(KeyCode.S) && isBlue || Input.GetKey(KeyCode.DownArrow) && isRed)
            if (rb.velocity.x < controller.maxSpeed.x)
                discreteActionsOut[2] = 1;

        if (Input.GetKey(KeyCode.A) && isBlue || Input.GetKey(KeyCode.LeftArrow) && isRed)
            if (Mathf.Abs(rb.velocity.z) < controller.maxSpeed.z)
                discreteActionsOut[0] = 1;

        if (Input.GetKey(KeyCode.D) && isBlue || Input.GetKey(KeyCode.RightArrow) && isRed)
            if (rb.velocity.z < controller.maxSpeed.z)
                discreteActionsOut[1] = 1;
    }
}
