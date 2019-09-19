using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    public static InventoryManager Inventory { get; private set; }

    private List<IGameManager> startSequenceManagers = new List<IGameManager>();

    private void Awake()
    {
        Inventory = GetComponent<InventoryManager>();
        startSequenceManagers.Add(Inventory);

        // Startup the managers asynchronously
        StartCoroutine(StartupManagers());
    }

    /// <summary>
    /// Iterates through all the managers and calls thier Startup commands
    /// And writes their status to the console
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartupManagers()
    {
        // Startup each manager
        foreach (IGameManager manager in startSequenceManagers)
        {
            manager.Startup();
        }

        // Pause the operation for one frame to allow other scripts to execute
        yield return null;

        int numModules = startSequenceManagers.Count;
        int numReady = 0;

        while (numReady < numModules)
        {

            foreach (IGameManager manager in startSequenceManagers)
            {
                if (manager.Status == ManagerStatus.Started)
                    numReady++;
            }

            Debug.Log($"Progress: {numReady}/{numModules}");

            // Pause the operation for one frame before checking the managers statuses again
            yield return null;
        }

        Debug.Log("All managers started");
    }
}
