using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FlightOtherPrice
    {
       

        #region 字段
        /// <summary>
        /// 
        /// </summary>
        private Int32 _Otherpriceid;

        /// <summary>
        /// 价格来源
        /// </summary>
        private Int32? _Sourcetype;

        /// <summary>
        /// 出发地
        /// </summary>
        private String _Departure;

        /// <summary>
        /// 目的地
        /// </summary>
        private String _Arrival;

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime? _CreateDate;

        /// <summary>
        /// 最低价格
        /// </summary>
        private Double? _LowestPrice;

        /// <summary>
        /// 航空公司
        /// </summary>
        private String _Airliner;

        /// <summary>
        /// 航班号
        /// </summary>
        private String _Flight;

        /// <summary>
        /// 舱位
        /// </summary>
        private String _Cabin;

        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public Int32 Otherpriceid
        {
            get { return _Otherpriceid; }
            set { this._Otherpriceid = value; }
        }

        /// <summary>
        /// 价格来源
        /// </summary>
        public Int32? Sourcetype
        {
            get { return _Sourcetype; }
            set { this._Sourcetype = value; }
        }

        /// <summary>
        /// 出发地
        /// </summary>
        public String Departure
        {
            get { return _Departure; }
            set { this._Departure = value; }
        }

        /// <summary>
        /// 目的地
        /// </summary>
        public String Arrival
        {
            get { return _Arrival; }
            set { this._Arrival = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set { this._CreateDate = value; }
        }

        /// <summary>
        /// 最低价格
        /// </summary>
        public Double? LowestPrice
        {
            get { return _LowestPrice; }
            set { this._LowestPrice = value; }
        }

        /// <summary>
        /// 航空公司
        /// </summary>
        public String Airliner
        {
            get { return _Airliner; }
            set { this._Airliner = value; }
        }

        /// <summary>
        /// 航班号
        /// </summary>
        public String Flight
        {
            get { return _Flight; }
            set { this._Flight = value; }
        }

        /// <summary>
        /// 舱位
        /// </summary>
        public String Cabin
        {
            get { return _Cabin; }
            set { this._Cabin = value; }
        }

        #endregion
    }
}
