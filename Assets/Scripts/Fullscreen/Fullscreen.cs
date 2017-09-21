using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{
    public class Fullscreen : MonoBehaviour
    {
        private string[] tips_table_ =
        {
        "None",
        "正在获取信息。。。",
        "网络延时等待中。。。",
    };
        private int tip_index_ = 1;
        public int TipIndex
        {
            get
            {
                return tip_index_;
            }
            set
            {
                tip_index_ = value;
            }
        }

        private Transform canvas_;
        private Text tips_;

        private void Awake()
        {
            canvas_ = transform.Find("Canvas");

            tips_ = canvas_.Find("Text").GetComponent<Text>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            tips_.text = tips_table_[tip_index_];
        }

        void OnEnable()
        {
            canvas_.gameObject.SetActive(true);
        }

        void OnDisable()
        {
            canvas_.gameObject.SetActive(false);
        }

    }

}