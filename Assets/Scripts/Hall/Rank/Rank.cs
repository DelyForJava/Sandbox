using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bean.Hall
{
    public class Rank : MonoBehaviour
    {
        private short info;

        public bool GetInfo(short info,short n)
        {
            bool ret = false;
            ret = (info>>(n-1) & 0x0001) == 0 ? false : true;
            return ret;
        }

        public BaseTableView baseTableView;

        List<string> data;
        private void Start()
        {
            Debug.LogMsg("first1:" + GetInfo(0x0007,1) );
            Debug.LogMsg("first2:" + GetInfo(0x0007, 2) );
            Debug.LogMsg("first3:" + GetInfo(0x0007, 3) );
            Debug.LogMsg("first4:" + GetInfo(0x0007, 4) );

        }

        private void Update()
        {
            //baseTableView.ReloadData();
        }

        private void UpdateCellAtIndex(BaseTableViewCell cell)
        {
            var c = cell as RankTableViewCell;
            if (!c)
                return;
            Debug.LogMsg("I am cell BaseIndex at:" + c.BaseIndex);
            //Debug.LogMsg("I am cell MemberName at:" + c.MemberName);
        }

    }

}