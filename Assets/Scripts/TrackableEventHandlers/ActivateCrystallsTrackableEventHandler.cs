using UnityEngine;
using UnityEngine.UI;

public class ActivateCrystallsTrackableEventHandler : DefaultTrackableEventHandler
{
    [SerializeField] private Button startButton;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        var updater = GetComponent<CrystallPositionUpdater>();

        startButton.gameObject.SetActive(true);

        updater.Init();
        updater.enabled = true;
    }

    protected override void OnTrackingLost()
    {

    }
}
