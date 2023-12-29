using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    private Transform tr;
    private RaycastHit hit; // ray�� ������ ���� ��ġ�� �����ϱ� ���� ����
    public float rotSpeed = 5.0f; //�ͷ��� ȸ���ӵ�
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        //���� ī�޶� ���콺 Ŀ���� ��ġ�� ĳ���õǴ� ���̸� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //������ Ray�� Scene �信 ��� �������� ǥ��
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
        //8��° ���̾�(TERRAIN)�� ���̰� �ε����ٸ�
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            //Ray�� ���� ��ġ�� ������ǥ�� ��ȯ
            Vector3 relative = tr.InverseTransformPoint(hit.point);
            //��ź��Ʈ �Լ��� �� �� �� ������ �Ի�
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            //rotSpeed ������ ������ �ӵ��� ȸ��
            tr.Rotate(0, angle * Time.deltaTime * rotSpeed, 0);
        }
    }
}
