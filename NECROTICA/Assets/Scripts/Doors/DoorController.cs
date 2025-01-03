using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float lowerDistance = 3f; 
    [SerializeField] private float lowerSpeed = 2f; 
    [SerializeField] private float activationDistance = 2f; 
    [SerializeField] private KeyCode activationKey = KeyCode.Space; 

    private Vector3 initialPosition;
    private Vector3 targetPosition; 
    private bool isLowering = false;
    private Collider doorCollider; 

    void Start()
    {
        initialPosition = transform.position;

        targetPosition = initialPosition - new Vector3(0, lowerDistance, 0);
        doorCollider = GetComponent<Collider>();
        if (doorCollider == null)
        {
            Debug.LogError("��������� ����� �� ������!");
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= activationDistance && Input.GetKeyDown(activationKey) && !isLowering)
        {
            Effect.instance.ScreenShake();
            AudioManager.instance.Play("BasicDoor");
            
            isLowering = true;
            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }
        }

        if (isLowering)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, lowerSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isLowering = false;
            }
        }
    }
}
