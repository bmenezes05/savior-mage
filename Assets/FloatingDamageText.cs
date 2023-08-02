using System.Collections;
using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifeTime = 1f;
    public Vector3 offset = new Vector3(0f, 1f, 0f);
    public Color textColor = Color.red;

    private TextMeshPro textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void ShowDamage(int damageAmount)
    {        
        textMesh.text = damageAmount.ToString();       
        textMesh.color = textColor;
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;
        Color initialColor = textMesh.color;
        Color transparentColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (elapsedTime < lifeTime)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            textMesh.color = Color.Lerp(initialColor, transparentColor, elapsedTime / lifeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
