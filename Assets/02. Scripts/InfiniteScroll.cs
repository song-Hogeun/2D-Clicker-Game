using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [Header("이어 붙일 타일들 (왼/중/오 순서 상관 없음)")]
    [SerializeField] private Transform[] tiles;   // Cloud Center/Left/Right 혹은 Ground Center/Left/Right

    [Header("이동 속도 (오른쪽 +, 왼쪽 -)")]
    [SerializeField] private float speed = 0.5f;

    private float _tileWidth;
    private float _rightLimitX;

    private void Start()
    {
        if (tiles == null || tiles.Length == 0)
        {
            Debug.LogError("InfiniteScroll : tiles가 비어있습니다.");
            return;
        }

        // 타일 한 개의 가로 길이 구하기 (SpriteRenderer 기준)
        var sr = tiles[0].GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            _tileWidth = sr.bounds.size.x;
        }
        else
        {
            Debug.LogError("InfiniteScroll : 첫 타일에 SpriteRenderer가 없습니다.");
            _tileWidth = 5f; // 임시값
        }

        // 카메라 오른쪽 끝보다 조금 더 오른쪽을 리셋 기준으로 사용
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;
        _rightLimitX = cam.transform.position.x + camWidth / 2f + _tileWidth;
    }

    private void Update()
    {
        if (tiles == null || tiles.Length == 0) return;

        float move = speed * Time.deltaTime;

        // 1) 타일 전체를 오른쪽으로 이동
        foreach (var t in tiles)
        {
            t.Translate(Vector3.right * move);
        }

        // 2) 현재 가장 왼쪽 타일 찾기
        Transform leftMost = tiles[0];
        for (int i = 1; i < tiles.Length; i++)
        {
            if (tiles[i].position.x < leftMost.position.x)
                leftMost = tiles[i];
        }

        // 3) 오른쪽 화면 밖으로 나간 타일을 가장 왼쪽 타일의 왼쪽으로 재배치
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].position.x >= _rightLimitX)
            {
                Vector3 pos = tiles[i].position;
                pos.x = leftMost.position.x - _tileWidth;  // 가장 왼쪽 타일 왼쪽에 이어 붙이기
                tiles[i].position = pos;

                // 이제 이 타일이 가장 왼쪽 타일이 됨
                leftMost = tiles[i];
            }
        }
    }
}
