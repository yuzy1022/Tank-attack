using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackAnim : MonoBehaviour
{
    //�ؽ�ó�� ȸ�� �ӵ�
    private float scrollSpeed = 1.0f;
    private Renderer _renderer; //������ ������Ʈ
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed * Input.GetAxisRaw("Vertical");
        // SetTextureOffset : �ؽ�ó�� �����°��� ����
        // SetTextureOffset�� ù��°�μ�: ����Ƽ ��Ʈ�� ���̴��� ������� ��
        //����� ���ڿ� _MainText: Diffuse(��ü�� ���̰�, ��ü��)
        //����� ���ڿ� _BumpMap: Normal map(������ ���̰�, ���ݻ縦 �ٲ�)
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        _renderer.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));
    }
}
