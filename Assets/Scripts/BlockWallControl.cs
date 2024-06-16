using UnityEngine;

public class BlockWallControl : MonoBehaviour
{
    [SerializeField] private GameObject[] walls;

    public void Set(bool active)
    {
        foreach (var wall in walls)
            wall.gameObject.SetActive(active);
    }
}
