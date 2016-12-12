using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ImageFader : MonoBehaviour
{

    public bool start = false;
    public GameObject nextObject;

    public float nextImageTime = 3f;
    public float duration = 3f;
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    public Color startColor = Color.clear;
    public Color displayColor = Color.white;
    public Color endColor = Color.clear;

    public GameObject textObject;

    public UnityEvent OnEnd;

    Image image;

    void Start()
    {
        if (start)
            StartImage();

    }

    /*void OnEnable()
	{
		StartImage ();
	}*/

    public void StartImage()
    {
        image = GetComponent<Image>();

        image.color = Color.clear;

        SetTextActive(false);

        gameObject.SetActive(true);

        StartCoroutine(ImageCoroutine());

        if (nextObject != null)
            StartCoroutine(NextImageCoroutine());
    }

    IEnumerator ImageCoroutine()
    {
        //Fadein
        float startTime = Time.time;
        float endTime = startTime + fadeInDuration;

        while (Time.time <= endTime)
        {
            yield return null;

            float t = Mathf.InverseLerp(startTime, endTime, Time.time);
            image.color = Color.Lerp(startColor, displayColor, t);

        }

        SetTextActive(true);

        //Wait
        float waitTime = duration - fadeInDuration - fadeOutDuration;
        yield return new WaitForSeconds(waitTime);

        SetTextActive(false);

        //Fadeout
        startTime = Time.time;
        endTime = startTime + fadeOutDuration;

        while (Time.time <= endTime)
        {
            yield return null;

            float t = Mathf.InverseLerp(startTime, endTime, Time.time);
            image.color = Color.Lerp(displayColor, endColor, t);

        }

        OnEnd.Invoke();

    }

    IEnumerator NextImageCoroutine()
    {
        yield return new WaitForSeconds(nextImageTime);

        if (nextObject != null)
        {
            nextObject.SetActive(true);//.StartImage ();
            ImageFader img = nextObject.GetComponent<ImageFader>();
            if (img != null)
                img.StartImage();
        }

    }

    void SetTextActive(bool active)
    {
        /*foreach (Transform t in transform)
			t.gameObject.SetActive (active);*/
        if (textObject != null)
            textObject.SetActive(active);
    }
}
