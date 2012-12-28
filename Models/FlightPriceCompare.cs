using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FlightPriceCompare
    {
        #region 字段
        /// <summary>
        /// CompareId
        /// </summary>
        private Int32 _Compareid;

        /// <summary>
        /// OtherPriceId
        /// </summary>
        private Int32? _Otherpriceid;

        /// <summary>
        /// 最低价格id
        /// </summary>
        private Int32? _LowestPriceId;

        /// <summary>
        /// 自动核实结果
        /// </summary>
        private Int32? _Autoresult;

        /// <summary>
        /// 核实结果
        /// </summary>
        private Int32? _Handresult;

        /// <summary>
        /// 备注
        /// </summary>
        private String _Memo;

        #endregion

        #region 属性
        /// <summary>
        /// CompareId
        /// </summary>
        public Int32 Compareid
        {
            get { return _Compareid; }
            set { this._Compareid = value; }
        }

        /// <summary>
        /// OtherPriceId
        /// </summary>
        public Int32? Otherpriceid
        {
            get { return _Otherpriceid; }
            set { this._Otherpriceid = value; }
        }

        /// <summary>
        /// 最低价格id
        /// </summary>
        public Int32? LowestPriceId
        {
            get { return _LowestPriceId; }
            set { this._LowestPriceId = value; }
        }

        /// <summary>
        /// 自动核实结果
        /// </summary>
        public Int32? Autoresult
        {
            get { return _Autoresult; }
            set { this._Autoresult = value; }
        }

        /// <summary>
        /// 核实结果
        /// </summary>
        public Int32? Handresult
        {
            get { return _Handresult; }
            set { this._Handresult = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public String Memo
        {
            get { return _Memo; }
            set { this._Memo = value; }
        }

        #endregion
    }
}
