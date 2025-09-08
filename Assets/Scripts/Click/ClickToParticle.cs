using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ParticleSystem))]
public class ClickToParticle : MonoBehaviour
{
    public bool ignoreUI = true; // UI 위 클릭 무시

    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // 마우스 왼쪽 클릭
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        // UI 위 클릭은 무시
        if (ignoreUI && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 0f; // 2D니까 깊이 0
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;  // 확실히 z=0 평면에 고정

        // 파티클 위치 이동 후 재생
        transform.position = worldPos + Vector3.up * 0.3f; // 마우스보다 살짝 위로
        var main = ps.main;
        main.startColor = Color.HSVToRGB(Random.value, 1f, 1f); // 클릭마다 색 변화
        ps.Play();
    }

    
}

