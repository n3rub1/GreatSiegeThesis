using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private bool deathLocked;

    public bool TryLockDeath()
    {
        if (deathLocked) return false;
        deathLocked = true;
        return true;
    }

    public void UnlockDeath()
    {
        deathLocked = false;
    }
}

