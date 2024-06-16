using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScreenShakeControl : MonoBehaviour
{
    private bool isActive;
    private bool isRunning = false;

    private Vector3 originalPosition;

    public void Set(bool active)
    {
        isActive = active;
        if (!active && isRunning)
        {
            Camera.main.transform.position = originalPosition;
            isRunning = false;
            FunctionUpdater.StopAllUpdatersWithName("CAMERA_SHAKE");
        }
    }

    private void Start()
    {
        originalPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        if (isActive)
        {
            if (!isRunning)
                StartCoroutine(ShakeCam(() =>
                {
                    Camera.main.transform.position = originalPosition;
                }));
        }
        else
        {
            Camera.main.transform.position = originalPosition;
        }
    }

    private IEnumerator ShakeCam(Action onComplete)
    {
        if (!isRunning)
        {
            isRunning = true;
            Shake(0.15f, 0.2f); 
            yield return new WaitForSeconds(1.5f);
            isRunning = false;
            FunctionUpdater.StopAllUpdatersWithName("CAMERA_SHAKE");
            Camera.main.transform.position = originalPosition;
            onComplete?.Invoke();
        }
    }

    private void Shake(float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        FunctionUpdater.Create(delegate ()
        {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * intensity;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;

            if (timer <= 0f)
            {
                Camera.main.transform.position -= lastCameraMovement;
                return true;
            }

            return false;
        }, "CAMERA_SHAKE", true, true);
    }
}
