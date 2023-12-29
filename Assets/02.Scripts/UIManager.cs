using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text waveEnemyText, waveNotifyText, remainShellText, timeText;
    public GameObject waveNotifyTextOb;
    string heat="heatShell", he="heShell";
    public GameObject heatFrame, heFrame;
    FireCannon fireCannon;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake() {
        fireCannon = GameObject.Find("Tank").GetComponent<FireCannon>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = ClearTimeUI.CalTime(GameManager.time);
    }

    public void updateEnemyWaveText(int enemy, int wave)
    {
        waveEnemyText.text = "Wave : " + wave + "\nEnemy Left : " + enemy;
    }

    public void WaveNotify(int wave)
    {
        waveNotifyTextOb.SetActive(true);
        waveNotifyText.text = System.Convert.ToString(wave) + " Wave";
        Invoke("WaveTextNull", 1.5f);
    }

    void WaveTextNull(){ 
        waveNotifyTextOb.SetActive(false);
    }

    public void changeShell(string shell)
    {
        if(shell == heat)
        {
            heatFrame.SetActive(true);
            heFrame.SetActive(false);
        }
        else
        {
            heatFrame.SetActive(false);
            heFrame.SetActive(true);
        }
        updateRemainShellText();
    }

    public void updateRemainShellText()
    {
        if(fireCannon.useShell == heat)
        {
            remainShellText.text = "Remain : ∞";
        }
        else
        {
            remainShellText.text = "Remain : " + fireCannon.heShellRemain;
        }
    }
}
