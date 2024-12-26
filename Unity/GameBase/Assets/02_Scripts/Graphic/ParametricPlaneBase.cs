using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParametricPlaneBase : Plane
{
    [SerializeField] protected float depth = 1f;

    protected override Mesh Build()
    {
        // 원래의 Plane 모델을 생성
        var mesh = base.Build();

        // Plane 모델이 가진 정점의 높이를 재설정한다.
        var vertices = mesh.vertices;

        // 정점의 격자 위의 위치의 비율(0.0 ~ 1.0)을 산출하기 위한 행-열 개수의 역수
        var winv = 1f / (widthSegments - 1);
        var hinv = 1f / (heightSegments - 1);

        for (int y = 0; y < heightSegments; y++)
        {
            // 행의 위치 비율 (0.0 ~ 1.0)
            var ry = y * hinv;

            for (int x = 0; x < widthSegments; x++)
            {
                // 열의 위치 비율 (0.0 ~ 1.0)
                var rx = x * winv;

                int index = y * widthSegments + x;
                vertices[index].y = Depth(rx, ry);
            }
        }

        // 정점 위치의 재설정
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        // 법선 방향을 자동으로 재계산
        mesh.RecalculateNormals();

        return mesh;
    }

    protected abstract float Depth(float u, float v);
}
