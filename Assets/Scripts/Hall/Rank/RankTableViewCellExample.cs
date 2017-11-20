using UnityEngine.UI;

namespace Bean.Hall
{
    //Inherit from TableViewCell instead of MonoBehavior to use the GameObject
    //containing this component as a cell in a TableView
    public class RankTableViewCellExample : BaseTableViewCell
    {
        public Text MemberLabel;
        public string MemberName;
        public string MemberLevel;
        public string MemberUrl;

        private void Update()
        {
            base.Label.text = BaseName + BaseIndex.ToString();

            MemberLabel.text = MemberName;
        }
    }

}