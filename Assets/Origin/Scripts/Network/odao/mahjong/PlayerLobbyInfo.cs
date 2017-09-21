using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyInfo : MonoBehaviour {


    //iUserID:" + data.iUserID + ",accountId:" + data.accountId + ",serviceId:" + data.serviceId + ",szPasswdToken:" + data.szPasswdToken + ",cGender:" + data.cGender + ",cVipLv:" + data.cVipLv + ",llGameCoin:" + data.llGameCoin + ",llBankCoin:" + data.llBankCoin + ",llDiamondNum:" + data.llDiamondNum + ",llGoldNum:" + data.llGoldNum + ",szNickName:" + data.szNickName + ",szWXIconURL:" + data.szWXIconURL + ",szWXNickName:" + data.szWXNickName
    
    public int iUserID { get; set; }

    public int accountId { get; set; }

    public int serviceId { get; set; }

    public string szPasswdToken { get; set; }

    public sbyte cGender { get; set; }

    public sbyte cVipLv { get; set; }

    public long llGameCoin { get; set; }

    public long llBankCoin { get; set; }

    public long llDiamondNum { get; set; }

    public long llGoldBean { get; set; }

    public string szNickName { get; set; }

    public string szWXIconURL { get; set; }

    public string szWXNickName { get; set; }
    public int iVipExp { get; set; }
    public sbyte cLevel { get; set; }
    public sbyte iLevelExp { get; set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
