using UnityEngine.UI;

namespace Bean.Hall
{
    //Inherit from TableViewCell instead of MonoBehavior to use the GameObject
    //containing this component as a cell in a TableView
    public class BaseTableViewCell : Tacticsoft.TableViewCell
    {
        public Text Label;
        public float BaseHeight = 50;
        public string BaseName;

        public int BaseIndex { get; set; }

        public void Reset()
        {

        }

        private void Start()
        {
           
        }

        //private void Update()
        //{
        //    Label.text = BaseName + BaseIndex.ToString();

        //}

    }

}