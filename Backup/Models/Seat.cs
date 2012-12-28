using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /***********************************************************************
     * 模块:  Seat.cs
     * 作者:  rxh
     * 日期： 2009-08-24
     * 目的:  存储航班舱位
     * 修改：2010-12-03 添加子舱位标记
     ***********************************************************************/
    public class Seat
    {
        #region 字段
        private string cabin;
        private int count;
        private double price;
        private string subCanbin;
        #endregion

        #region 属性
        /// <summary>
        /// 舱位
        /// </summary>
        public string Cabin
        {
            get
            {
                return cabin;
            }
            set
            {
                if (this.cabin != value)
                    this.cabin = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (this.count != value)
                    this.count = value;
            }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (this.price != value)
                    this.price = value;
            }
        }

        /// <summary>
        /// 子舱位
        /// </summary>
        public string SubCanbin
        {
            get
            {
                return subCanbin;
            }
            set
            {
                if (this.subCanbin != value)
                    this.subCanbin = value;
            }
        }
        #endregion
    }
}
