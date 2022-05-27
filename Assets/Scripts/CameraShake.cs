using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    internal IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 startingPos = transform.localPosition;
        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            float shakeX = Random.RandomRange(-1, 1) * magnitude;

            
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(shakeX, startingPos.y, startingPos.z), 0.2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = startingPos;
    }
}
