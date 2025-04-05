using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public GameObject waterPrefab;
    public float flowSpeed = 0.5f;
    private HashSet<Vector2> waterPositions = new HashSet<Vector2>();
    private List<GameObject> waterBlocks = new List<GameObject>();
    public float destroyDelay = 0.1f;

    void Start()
    {
        StartFlow(transform.position); // 최초 물 블럭 시작
    }

    void StartFlow(Vector2 startPos)
    {
        GameObject firstWater = Instantiate(waterPrefab, startPos, Quaternion.identity);
        waterBlocks.Add(firstWater);
        waterPositions.Add(startPos);
        StartCoroutine(FlowRoutine());
    }

    IEnumerator FlowRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flowSpeed); // 흐름 속도에 맞게 대기

            List<GameObject> newWaterBlocks = new List<GameObject>();
            List<Vector2> newWaterPositions = new List<Vector2>();

            // 물 블럭이 삭제된 후 null 값이 남지 않도록 처리
            waterBlocks.RemoveAll(water => water == null);

            foreach (GameObject water in waterBlocks)
            {
                if (water == null) continue; // 이미 삭제된 물 블럭 건너뜀

                Vector2 currentPos = water.transform.position;

                // 장애물 감지 및 흐름 중지
                CheckForObstaclesAndStopFlow(currentPos); // 장애물 감지 후 흐름 멈추기

                // 아래쪽으로 흐름
                Vector2 downPos = currentPos + Vector2.down;
                if (!IsBlocked(downPos) && !waterPositions.Contains(downPos))  // 중복 위치 체크
                {
                    GameObject newWater = Instantiate(waterPrefab, downPos, Quaternion.identity, transform); // 부모 설정
                    newWaterBlocks.Add(newWater);
                    newWaterPositions.Add(downPos); // 새 위치 추가
                }

                // 위쪽으로 흐름
                Vector2 upPos = currentPos + Vector2.up;
                if (!IsBlocked(upPos) && !waterPositions.Contains(upPos))  // 중복 위치 체크
                {
                    GameObject newWater = Instantiate(waterPrefab, upPos, Quaternion.identity, transform); // 부모 설정
                    newWaterBlocks.Add(newWater);
                    newWaterPositions.Add(upPos); // 새 위치 추가
                }
            }

            // 새로운 물 블럭을 리스트에 추가
            waterBlocks.AddRange(newWaterBlocks);
            waterPositions.UnionWith(newWaterPositions); // 물 블럭의 새로운 위치들을 추가
        }
    }

    bool IsBlocked(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position);
        if (hit != null) // 막히는 블럭
        {
            if (hit.CompareTag("B_Fire") || hit.CompareTag("B_Blocked_Earth") || hit.CompareTag("OutWall"))
            {
                return true;
            }
        }
        return false;
    }

    //void CheckForObstaclesAndStopFlow(Vector2 position)
    //{
    //    RaycastHit2D[] hitsDown = Physics2D.RaycastAll(position, Vector2.down, 50f); // 아래쪽 레이캐스트
    //    RaycastHit2D[] hitsUp = Physics2D.RaycastAll(position, Vector2.up, 50f); // 위쪽 레이캐스트

    //    // 아래쪽 방향에서 모든 충돌체 감지
    //    foreach (RaycastHit2D hit in hitsDown)
    //    {
    //        if (hit.collider != null && hit.collider.CompareTag("B_Blocked_Earth"))
    //        {
    //            StopFlowBasedOnObstacle(hit.point, Vector2.down); // 장애물에서 흐름을 멈춤
    //            break; // 첫 번째 장애물에서 흐름을 멈춘 후 반복문 종료
    //        }
    //    }

    //    // 위쪽 방향에서 모든 충돌체 감지
    //    foreach (RaycastHit2D hit in hitsUp)
    //    {
    //        if (hit.collider != null && hit.collider.CompareTag("B_Blocked_Earth"))
    //        {
    //            StopFlowBasedOnObstacle(hit.point, Vector2.up); // 장애물에서 흐름을 멈춤
    //            break; // 첫 번째 장애물에서 흐름을 멈춘 후 반복문 종료
    //        }
    //    }
    //}

    //void StopFlowBasedOnObstacle(Vector2 obstaclePosition, Vector2 flowDirection)
    //{
    //    // 장애물 기준으로 흐르는 물 블럭들 삭제
    //    foreach (GameObject water in waterBlocks)
    //    {
    //        if (water == null) continue; // 이미 삭제된 물 블럭 건너뜀

    //        Vector2 currentPos = water.transform.position;

    //        // 흐름 방향에 따라 물 블럭 삭제 조건
    //        if (flowDirection == Vector2.down)
    //        {
    //            // 아래로 흐르던 물 블럭 중 장애물보다 '더 낮은' 물 블럭만 삭제
    //            if (currentPos.y < obstaclePosition.y && currentPos.y < transform.position.y)
    //            {
    //                Destroy(water); // 아래로 흐르던 물 블럭 삭제
    //            }
    //        }
    //        else if (flowDirection == Vector2.up)
    //        {
    //            // 위로 흐르던 물 블럭 중 장애물보다 '더 높은' 물 블럭만 삭제
    //            if (currentPos.y > obstaclePosition.y && currentPos.y > transform.position.y)
    //            {
    //                Destroy(water); // 위로 흐르던 물 블럭 삭제
    //            }
    //        }
    //    }
    //}

    void CheckForObstaclesAndStopFlow(Vector2 position)
    {
        RaycastHit2D[] hitsDown = Physics2D.RaycastAll(position, Vector2.down, 50f); // 아래쪽 레이캐스트
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(position, Vector2.up, 50f); // 위쪽 레이캐스트

        // 아래쪽 방향에서 모든 충돌체 감지
        foreach (RaycastHit2D hit in hitsDown)
        {
            if (hit.collider != null && hit.collider.CompareTag("B_Blocked_Earth"))
            {
                StartCoroutine(FadeOutAndDestroy(hit.point, Vector2.down)); // 장애물에서 흐름을 멈추고 서서히 삭제
                break; // 첫 번째 장애물에서 흐름을 멈춘 후 반복문 종료
            }
        }

        // 위쪽 방향에서 모든 충돌체 감지
        foreach (RaycastHit2D hit in hitsUp)
        {
            if (hit.collider != null && hit.collider.CompareTag("B_Blocked_Earth"))
            {
                StartCoroutine(FadeOutAndDestroy(hit.point, Vector2.up)); // 장애물에서 흐름을 멈추고 서서히 삭제
                break; // 첫 번째 장애물에서 흐름을 멈춘 후 반복문 종료
            }
        }
    }
    IEnumerator FadeOutAndDestroy(Vector2 obstaclePosition, Vector2 flowDirection)
    {
        List<GameObject> waterToDelete = new List<GameObject>();

        foreach (GameObject water in waterBlocks)
        {
            if (water == null) continue; // 이미 삭제된 물 블럭 건너뜀

            Vector2 currentPos = water.transform.position;

            // 흐름 방향에 따라 물 블럭 삭제 조건
            if (flowDirection == Vector2.down)
            {
                // 아래로 흐르던 물 블럭 중 장애물보다 '더 낮은' 물 블럭만 삭제
                if (currentPos.y < obstaclePosition.y && currentPos.y < transform.position.y)
                {
                    waterToDelete.Add(water);
                }
            }
            else if (flowDirection == Vector2.up)
            {
                // 위로 흐르던 물 블럭 중 장애물보다 '더 높은' 물 블럭만 삭제
                if (currentPos.y > obstaclePosition.y && currentPos.y > transform.position.y)
                {
                    waterToDelete.Add(water);
                }
            }
        }

        // 서서히 삭제하기
        foreach (GameObject water in waterToDelete)
        {
            Destroy(water); // 물 블럭 삭제
            yield return new WaitForSeconds(destroyDelay); // 일정 시간 후 삭제
        }
    }

}