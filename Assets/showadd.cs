
using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class showadd : MonoBehaviour
{
void Start ()
{
Advertisement.Initialize ("1110015", true);

StartCoroutine (ShowAdWhenReady ());
}

IEnumerator ShowAdWhenReady()
{
while (!Advertisement.isReady ())
yield return null;

Advertisement.Show ();
}
}