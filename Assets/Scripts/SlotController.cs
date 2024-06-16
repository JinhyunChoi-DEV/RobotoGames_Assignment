using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlotController : MonoBehaviour
{
    [SerializeField] private GameObject[] Slots;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image image;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private TMP_Text description;

    private string[] descriptions =
    {
        "Default Mode: There are no changes to the map.",
        "Blocking Mode: Obstacles appear on the map that block the ball's movement.",
        "Shaking Mode: The screen shakes periodically."
    };


    public void Close()
    {
        panel.SetActive(false);
    }

    public IEnumerator OpenSlot(Action<MapMode> setMode, Action onComplete)
    {
        panel.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        int resultIndex = Random.Range(0, Slots.Length - 1);
        GameObject lastSlot = Slots[Slots.Length - 1];
        float lastSlotY = lastSlot.gameObject.transform.localPosition.y;
        float fianlY = Slots[resultIndex].transform.localPosition.y;

        int MaxCount = 5;
        int count = 0;
        float y = 0.0f;
        while (count < MaxCount)
        {
            if (y < Mathf.Abs(lastSlotY))
            {
                targetObject.transform.localPosition += new Vector3(0.0f, 20.0f, 0.0f);
                y += 20;
            }
            else
            {
                targetObject.transform.localPosition = Vector3.zero;
                y = 0.0f;
                count++;
            }

            yield return new WaitForSeconds(0.025f);
        }

        y = 0.0f;
        while (y < Mathf.Abs(fianlY))
        {
            targetObject.transform.localPosition += new Vector3(0.0f, 20.0f, 0.0f);
            y += 20;

            yield return new WaitForSeconds(0.055f);
        }

        targetObject.transform.localPosition = new Vector3(0.0f, -fianlY, 0.0f);
        setMode?.Invoke((MapMode)resultIndex);

        image.sprite = Slots[resultIndex].gameObject.GetComponent<Image>().sprite;
        description.text = descriptions[resultIndex];

        yield return new WaitForSeconds(3.5f);

        Close();
        targetObject.transform.localPosition = Vector3.zero;
        image.sprite = baseSprite;
        description.text = "???";

        onComplete?.Invoke();
    }

    private void Start()
    {
        description.text = "???";
    }
}