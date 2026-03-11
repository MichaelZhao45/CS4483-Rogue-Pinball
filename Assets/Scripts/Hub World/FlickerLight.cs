using System.Collections;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    private bool _flickerStarted;
    [SerializeField] private Light[] _lights;

    public float minFlickerSpeed = 0.5f;
    public float maxFlickerSpeed = 1.0f;

    void Start()
    {
        _flickerStarted = false;
    }

    void Update()
    {
        if (!_flickerStarted) StartCoroutine(Flicker());
    }

    private void ToggleAllLights(bool status)
    {
        foreach (Light light in _lights)
        {
            light.enabled = status;
        }
    }

    public void ToggleOnOff()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        StopAllCoroutines();
        _flickerStarted = false;
    }

    IEnumerator Flicker()
    {
        _flickerStarted = true;

        ToggleAllLights(false);
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        ToggleAllLights(true);
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));

        _flickerStarted = false;
    }
}
