using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAnim : MonoBehaviour
{
    //텍스처의 회전 속도
    private float scrollSpeed = 1.0f;
    private Renderer _renderer; //렌더러 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed * Input.GetAxisRaw("Vertical");
        // SetTextureOffset : 텍스처의 오프셋값을 수정
        // SetTextureOffset의 첫번째인수: 유니티 빌트인 셰이더를 사용했을 때
        //사용할 문자열 _MainText: Diffuse(물체의 깊이감, 입체감)
        //사용할 문자열 _BumpMap: Normal map(평면상의 높이값, 빛반사를 바꿈)
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        _renderer.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));
    }
}
