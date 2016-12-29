using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ImageFader : MonoBehaviour
{

    public bool start = false;
    public GameObject nextObject;

	public bool keyToSkip = false;
	public bool keyToContinue = false;
    public float nextImageTime = 3f;
    public float duration = 3f;
	public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    public Color startColor = Color.clear;
    Color displayColor = Color.white;
    public Color endColor = Color.clear;

    public GameObject textObject;
    public float textDuration;

	public bool pauseOnDisplay = false;

	public UnityEvent OnEnd;
	public UnityEvent OnStart;
	public UnityEvent OnFinishFadeIn;
	public UnityEvent OnStartFadeOut;

	float startingTimeScale;

    Image image;

	/*void Awake()
	{
		if (!start)
			gameObject.SetActive(false);
	}*/

	void Start()
    {
		startingTimeScale = Time.timeScale;

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

        SetTextActive(false);

		displayColor = image.color;
		
		image.color = Color.clear;
		
        gameObject.SetActive(true);

        StartCoroutine(ImageCoroutine());

        if (nextObject != null)
            StartCoroutine(NextImageCoroutine());
    }

    IEnumerator ImageCoroutine()
    {
		OnStart.Invoke();

        //Fadein
        float startTime = Time.time;
        float endTime = startTime + fadeInDuration;

        while (Time.time <= endTime)
        {
            yield return null;

            float t = Mathf.InverseLerp(startTime, endTime, Time.time);
            image.color = Color.Lerp(startColor, displayColor, t);

        }

		OnFinishFadeIn.Invoke();

        SetTextActive(true);

        if (textDuration != 0)
        {
            //Remove text after given interval
            StartCoroutine(TextRemoveCoroutine());
        }

		if (pauseOnDisplay)
		{
			Time.timeScale = 0f;
		}

        //Wait
		if (keyToContinue)
		{
			while (!Input.anyKeyDown)
			{
				yield return null;
			}
		}
		else
		{
			float waitEndTime = Time.realtimeSinceStartup + duration - fadeInDuration - fadeOutDuration;
			while (Time.realtimeSinceStartup < waitEndTime)
			{
				if (keyToSkip && Input.anyKeyDown)
					break;
				
				yield return null;
			}
		}

		if (pauseOnDisplay)
		{
			Time.timeScale = 1f;
		}

        SetTextActive(false);

		OnStartFadeOut.Invoke();

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

		//gameObject.SetActive(false);
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

    IEnumerator TextRemoveCoroutine()
    {
        yield return new WaitForSeconds(textDuration);
        SetTextActive(false);
    }

    void SetTextActive(bool active)
    {
        /*foreach (Transform t in transform)
			t.gameObject.SetActive (active);*/
        if (textObject != null)
            textObject.SetActive(active);

    }

	void OnDisable()
	{
		Time.timeScale = 1f;
	}
}
