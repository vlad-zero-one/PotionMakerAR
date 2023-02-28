using UnityEngine;

public class CrystallPositionUpdater : MonoBehaviour
{
    [SerializeField] GameObject crystalls;

    private Transform crystallsTransform;

    private float zPosition;

    public void Init()
    {
        crystalls.SetActive(true);

        crystallsTransform = crystalls.GetComponent<Transform>();

        zPosition = transform.position.z;

        crystallsTransform.position = new Vector3(
            crystallsTransform.position.x,
            crystallsTransform.position.y,
            zPosition);
    }

    private void Update()
    {
        if (!this.enabled) return;

        if (Mathf.Abs(zPosition - transform.position.z) > 1)
        {
            zPosition = transform.position.z;
            crystallsTransform.position = new Vector3(
                crystallsTransform.position.x,
                crystallsTransform.position.y,
                zPosition);
        }
    }
}
