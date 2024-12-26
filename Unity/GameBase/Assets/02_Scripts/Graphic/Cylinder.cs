using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : ProceduralModelingBase
{
    [SerializeField, Range(0.1f, 10f)] protected float height = 3f, radius = 1f;
    [SerializeField, Range(3, 32)] protected int segments = 16;
    [SerializeField] private bool openEnded = true;

    const float PI2 = Mathf.PI * 2f;

    protected override Mesh Build()
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        // 위쪽의 높이와 아래쪽의 높이
        float top = height * 0.5f, bottom = -height * 0.5f;

        // 측면을 구성하는 정점 데이터 생성
        GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, true);

        // 측면 삼각형을 구성하는 원 위의 정점을 참고하기 위해,
        // index가 원을 한바퀴 돌기 위한 계산
        var len = (segments + 1) * 2;

        // 위쪽과 아래쪽을 이어서 측면을 구성
        for (int i = 0; i < segments + 1; i++)
        {
            int idx = i * 2;
            int a = idx, b = idx + 1, c = (idx + 2) % len, d = (idx + 3) % len;
            triangles.Add(a);
            triangles.Add(c);
            triangles.Add(b);

            triangles.Add(d);
            triangles.Add(b);
            triangles.Add(c);
        }

        // 위쪽과 아래쪽에 뚜껑을 생성
        if (openEnded)
        {
            // 뚜껑의 모델을 위한 정점은 라이팅시 다른 법선을 이용하기 위해 측면과 공유하지 않고 새롭게 추가한다.
            GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, false);

            // 위쪽 뚜껑 한가운데의 정점
            vertices.Add(new Vector3(0f, top, 0f));
            uvs.Add(new Vector2(0.5f, 1f));
            normals.Add(new Vector3(0f, 1f, 0f));

            // 아래쪽 뚜껑 한가운데의 정점
            vertices.Add(new Vector3(0f, bottom, 0f)); // bottom
            uvs.Add(new Vector2(0.5f, 0f));
            normals.Add(new Vector3(0f, -1f, 0f));

            var it = vertices.Count - 2;
            var ib = vertices.Count - 1;

            // 측면 정점들의 index를 참조하지 않기 위한 offset var
            var offset = len;

            // 위쪽 뚜껑의 면
            for (int i = 0; i < len; i += 2)
            {
                triangles.Add(it);
                triangles.Add((i + 2) % len + offset);
                triangles.Add(i + offset);
            }

            // 아래쪽 뚜껑의 면
            for (int i = 1; i < len; i += 2)
            {
                triangles.Add(ib);
                triangles.Add(i + offset);
                triangles.Add((i + 2) % len + offset);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();

        return mesh;
    }

    void GenerateCap(int segments, float top, float bottom, float radius, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, bool side)
    {
        for (int i = 0; i < segments; i++)
        {
            // 0.0 ~ 1.0
            float ratio = (float)i / (segments - 1);

            // 0.0 ~ 2π
            float rad = ratio * PI2;

            // 원주에 균등하게 위쪽과 아래쪽에 정점을 배치한다.
            float cos = Mathf.Cos(rad), sin = Mathf.Sin(rad);
            float x = cos * radius, z = sin * radius;
            Vector3 tp = new Vector3(x, top, z), bp = new Vector3(x, bottom, z);

            // 위쪽
            vertices.Add(tp);
            uvs.Add(new Vector2(ratio, 1f));

            // 아래쪽
            vertices.Add(bp);
            uvs.Add(new Vector2(ratio, 0f));

            if (side)
            {
                // 측면의 바깥쪽을 향하는 법선
                var normal = new Vector3(cos, 0f, sin);
                normals.Add(normal);
                normals.Add(normal);
            }
            else
            {
                normals.Add(new Vector3(0f, 1f, 0f)); // 뚜껑의 위를 향하는 법선
                normals.Add(new Vector3(0f, -1f, 0f)); // 뚜껑의 아래를 향하는 법선
            }
        }

    }


}
