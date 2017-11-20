using UnityEngine;

namespace Bean.Hall
{
    //An example implementation of a class that communicates with a TableView
    public class BaseTableView : MonoBehaviour, Tacticsoft.ITableViewDataSource
    {
        public GameObject ManagerGameObject;
        public GameObject Cell;
        private BaseTableViewCell baseTableViewCell;

        public Tacticsoft.TableView TableView;

        public int NumRows;
        private int numCreated = 0;
        
        public void ReloadData()
        {
            TableView.dataSource = this;

        }

        public void Start()
        {
            TableView.dataSource = this;
            baseTableViewCell = Cell.GetComponent<BaseTableViewCell>();
            if(!baseTableViewCell)
            {
                baseTableViewCell = Cell.AddComponent<BaseTableViewCell>();
            }

        }

        //Will be called by the TableView to know how many rows are in this table
        public int GetNumberOfRowsForTableView(Tacticsoft.TableView tableView)
        {
            return NumRows;
        }

        //Will be called by the TableView to know what is the height of each row
        public float GetHeightForRowInTableView(Tacticsoft.TableView tableView, int row)
        {
                return baseTableViewCell.BaseHeight;
        }

        //Will be called by the TableView when a cell needs to be created for display


        public Tacticsoft.TableViewCell GetCellForRowInTableView(Tacticsoft.TableView tableView, int row)
        {
            var cell = tableView.GetReusableCell(baseTableViewCell.reuseIdentifier) as BaseTableViewCell;
            if (cell == null)
            {
                cell = Instantiate(baseTableViewCell);
                cell.name = "Cell_" + (++numCreated).ToString();
            }
            cell.BaseIndex = row;
            if (ManagerGameObject)
            {
                ManagerGameObject.SendMessage("UpdateCellAtIndex", cell, SendMessageOptions.DontRequireReceiver);
            }
            return cell;
        }

    }

}